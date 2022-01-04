using SI.Biz.Core.DatabaseExecution;
using SI.Biz.Core.Events;
using SI.Util;
using SI.Util.Events;
using System;
using System.Xml;

namespace Fujitsu.Edoc.EventHandlers
{
    [BizEventSetup]
    public class ClasscodeHistoryEventHandler : IEventSetup
    {
        public void Initialize(string configXml)
        {
            EventShimFactory.GetEventShim<CaseManagerEventShim>().AfterInsert += new SystemEventHandler<MetaOperationEventArgs>(CaseManager_AfterInsert);
            EventShimFactory.GetEventShim<CaseManagerEventShim>().AfterUpdate += new SystemEventHandler<MetaOperationEventArgs>(CaseManager_AfterUpdate);
        }

        private void CaseManager_AfterInsert(MetaOperationEventArgs eventArgs)
        {
            LogTools.TraceLine("Fujitsu.Edoc.EventHandlers.ClasscodeHistoryEventHandler.CaseManager_AfterInsert called");
            XmlNode batchNode = eventArgs.Operation.Statement.BatchNode;
            StoreUsedClasscode(batchNode);
        }

        private void CaseManager_AfterUpdate(MetaOperationEventArgs eventArgs)
        {
            LogTools.TraceLine("Fujitsu.Edoc.EventHandlers.ClasscodeHistoryEventHandler.CaseManager_AfterUpdate called");
            XmlNode batchNode = eventArgs.Operation.Statement.BatchNode;
            StoreUsedClasscode(batchNode);
        }

        private void StoreUsedClasscode(XmlNode batchNode)
        {
            try
            {
                string owner = "fujitsu";
                string key = "classificationplan";
                string value = (string)SI.Biz.Core.SettingsCache.GetValue(owner, key);

                switch (value)
                {
                    case "1": // KLE
                        StoreUsedClasscodeKLE(batchNode);
                        return;
                    case "2": //RN
                        StoreUsedClasscodeRN(batchNode);
                        return;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                LogTools.TraceLine("ERROR: " + msg);
            }
        }

        private void StoreUsedClasscodeKLE(XmlNode batchNode)
        {
            try
            {
                string userRecno = SI.Biz.Core.Fluent.Get.User.ContactId;

                // Find the classcode
                string facetClasscodeID = "0";
                XmlNodeList noarkNodes = batchNode.SelectNodes("INSERTSTATEMENT[@ENTITY='connection: Case - Noark Classification code']");
                XmlNode facetNode = batchNode.SelectSingleNode("INSERTSTATEMENT[@ENTITY='connection: Case - Noark Classification code'][METAITEM[@NAME='ClassTypeID']=203000]");
                if (facetNode != null)
                {
                    facetClasscodeID = facetNode.SelectSingleNode("METAITEM[@NAME='ToClassCode']").InnerText;
                }

                foreach (XmlNode noarkNode in noarkNodes)
                {
                    XmlNode valueNode = noarkNode.SelectSingleNode("METAITEM[@NAME='Value']");
                    if (valueNode != null && valueNode.InnerText.Length == 8)
                    {
                        XmlNode ToClassCodeNode = noarkNode.SelectSingleNode("METAITEM[@NAME='ToClassCode']");

                        string classcodeID = ToClassCodeNode.InnerText;
                        string contactID = userRecno;

                        // Delete the classcode from the history so it can be inserted with a new date
                        string sqlDeleteStatement = "delete from fu_ch_clscodehist where fu_ch_ct_recno=" + contactID + " and fu_ch_nov_recno=" + classcodeID + " and fu_ch_nov_facetrecno=" + facetClasscodeID;
                        LogTools.TraceLine(sqlDeleteStatement);
                        object resultDelete = DBExecuter.ExecuteSql(sqlDeleteStatement);

                        // Make sure that there is only 9 elements left in the history for current user
                        string sqlSelectStatement = "select fu_ch_nov_recno, fu_ch_nov_facetrecno, fu_ch_date from fu_ch_clscodehist where fu_ch_ct_recno=" + contactID + " order by fu_ch_date asc";
                        LogTools.TraceLine(sqlDeleteStatement);
                        object[,] resultSelect = (object[,])DBExecuter.ExecuteSql(sqlSelectStatement);
                        int numberOfRows = resultSelect == null ? 0 : resultSelect.GetUpperBound(1) + 1;
                        for (int i = 0; i < numberOfRows - 9; i++)
                        {
                            sqlDeleteStatement = "delete from fu_ch_clscodehist where fu_ch_ct_recno=" + contactID + " and fu_ch_nov_recno=" + resultSelect[0, i].ToString() + " and fu_ch_nov_facetrecno=" + resultSelect[1, i].ToString();
                            LogTools.TraceLine(sqlDeleteStatement);
                            resultDelete = DBExecuter.ExecuteSql(sqlDeleteStatement);
                        }

                        // Insert the classcode in the history
                        string sqlInsertStatement = "insert into fu_ch_clscodehist (fu_ch_ct_recno, fu_ch_nov_recno, fu_ch_nov_facetrecno) values (" + contactID + ", " + classcodeID + ", " + facetClasscodeID + ")";
                        LogTools.TraceLine(sqlInsertStatement);
                        object resultInsert = DBExecuter.ExecuteSql(sqlInsertStatement);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                LogTools.TraceLine("ERROR: " + msg);
            }
        }

        private void StoreUsedClasscodeRN(XmlNode batchNode)
        {
            try
            {
                // Get the current user
                string userRecno = SI.Biz.Core.Fluent.Get.User.ContactId;

                // Find the classcode with the higerst sort
                XmlNodeList noarkNodes = batchNode.SelectNodes("INSERTSTATEMENT[@ENTITY='connection: Case - Noark Classification code']");

                int sort = 0;
                string classcodeID = "";
                foreach (XmlNode noarkNode in noarkNodes)
                {
                    XmlNode sortNode = noarkNode.SelectSingleNode("METAITEM[@NAME='Sort']");
                    if (int.Parse(sortNode.InnerText) > sort)
                    {
                        sort = int.Parse(sortNode.InnerText);
                        XmlNode ToClassCodeNode = noarkNode.SelectSingleNode("METAITEM[@NAME='ToClassCode']");
                        classcodeID = ToClassCodeNode.InnerText;
                    }
                }

                if (!string.IsNullOrEmpty(classcodeID))
                {
                    string contactID = userRecno;

                    // Delete the classcode from the history so it can be inserted with a new date
                    string sqlDeleteStatement = "delete from fu_ch_clscodehist where fu_ch_ct_recno=" + contactID + " and fu_ch_nov_recno=" + classcodeID;
                    LogTools.TraceLine(sqlDeleteStatement);
                    object resultDelete = DBExecuter.ExecuteSql(sqlDeleteStatement);

                    // Make sure that there is only 9 elements left in the history for current user
                    string sqlSelectStatement = "select fu_ch_nov_recno, fu_ch_nov_facetrecno, fu_ch_date from fu_ch_clscodehist where fu_ch_ct_recno=" + contactID + " order by fu_ch_date asc";
                    LogTools.TraceLine(sqlDeleteStatement);
                    object[,] resultSelect = (object[,])DBExecuter.ExecuteSql(sqlSelectStatement);
                    int numberOfRows = resultSelect == null ? 0 : resultSelect.GetUpperBound(1) + 1;
                    for (int i = 0; i < numberOfRows - 9; i++)
                    {
                        sqlDeleteStatement = "delete from fu_ch_clscodehist where fu_ch_ct_recno=" + contactID + " and fu_ch_nov_recno=" + resultSelect[0, i].ToString() + " and fu_ch_nov_facetrecno=" + resultSelect[1, i].ToString();
                        LogTools.TraceLine(sqlDeleteStatement);
                        resultDelete = DBExecuter.ExecuteSql(sqlDeleteStatement);
                    }

                    // Insert the classcode in the history
                    string sqlInsertStatement = "insert into fu_ch_clscodehist (fu_ch_ct_recno, fu_ch_nov_recno, fu_ch_nov_facetrecno) values (" + contactID + ", " + classcodeID + ", 0)";
                    LogTools.TraceLine(sqlInsertStatement);
                    object resultInsert = DBExecuter.ExecuteSql(sqlInsertStatement);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                LogTools.TraceLine("ERROR: " + msg);
            }
        }
    }
}