<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassCodeAdmin.aspx.cs" Inherits="Fujitsu.eDoc.ClassCodeAdmin.ClassCodeAdmin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administration af stikord</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div style="margin:20px;">

        <asp:Panel runat="server" ID="panelError" Visible="false" CssClass="err">
            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
        </asp:Panel>

        <asp:Panel runat="server" ID="MainContent" Visible="False">

            <div style="padding-bottom:20px;">
                <span style="font-size:22px;">Administration af følgende stikord:</span><br />
                <asp:Label runat="server" ID="lblClassCode" CssClass="classcode_title"></asp:Label>
            </div>

            <div style="padding-bottom:20px;">
                Følgende organisationsenheder skal have adgang<br /> til dette stikord som "relevant".
            </div>

            <asp:label runat="server" ID="lblUpdated" Visible="false" CssClass="good">
                Opdateret kl. <%= DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") %><br /><br />
            </asp:label>

            <telerik:RadTreeView    runat="server"
                                    ID="treeOrganisations"
                                    DataFieldID="ContactRecno"
                                    DataFieldParentID="ParentRecno"
                                    DataTextField="ContactName"
                                    DataValueField="ContactRecno"
                                    CheckBoxes="true"
                                    MultipleSelect="true"
                                    CheckChildNodes="true"
                                    ShowLineImages="true">
            </telerik:RadTreeView>

            <div style="padding-top:20px;">
                <asp:Button runat="server" ID="btnUpdate" UseSubmitBehavior="true" Text="Opdatér" style="margin-right:20px;" />
                <asp:Button runat="server" ID="btnClose" UseSubmitBehavior="false" Text="Luk vindue" OnClientClick="javascript:self.close();" />
            </div>

        </asp:Panel>

    </div>
    </form>
</body>
</html>
