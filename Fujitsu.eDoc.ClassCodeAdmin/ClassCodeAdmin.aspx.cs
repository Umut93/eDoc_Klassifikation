using System.Web;
//using Fujitsu.eDoc.SIData.Managers;
//using Fujitsu.eDoc.SIData.ClassTypes;

namespace Fujitsu.eDoc.ClassCodeAdmin
{
    public partial class ClassCodeAdmin : System.Web.UI.Page
    {
        //test
        protected string mCode = "";
        protected string mFacet = "";
        protected string mCodeID = "";
        protected string mFacetID = "";

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    btnUpdate.Click += new EventHandler(btnUpdate_Click);
        //    treeOrganisations.NodeExpand += new RadTreeViewEventHandler(treeOrganisations_NodeExpand);

        //    // CHECK FOR USER RIGHTS
        //    //
        //    if (UserManager.GetUserCurrentRoleID() == 0 || UserManager.GetUserCurrentRoleID() == 8)
        //        MainContent.Visible = true;
        //    else
        //    {
        //        lblErrorMessage.Text = "Du har ikke rettigheder til denne side";
        //        panelError.Visible = true;
        //    }

        //    try
        //    {
        //        // Get query values
        //        GetQuery();

        //        // Set headline
        //        lblClassCode.Text = mCode + " - " + mFacet;

        //        // Bind data
        //        //if (!IsPostBack)
        //        //    BindData();
        //    }

        //    catch (Exception exp)
        //    {
        //        // ERROR MESSAGE
        //        lblErrorMessage.Text = "Fejl på siden";
        //        MainContent.Visible = false;
        //        panelError.Visible = true;

        //        throw new Exception(exp.Message);
        //    }

        //}

        //void treeOrganisations_NodeExpand(object sender, RadTreeNodeEventArgs e)
        //{
        //    List<FOrganisation> listOrganisations = new ContactManager().GetOrganisationsByParent(Convert.ToInt32(e.Node.Value));

        //    foreach (FOrganisation org in listOrganisations)
        //    {
        //        RadTreeNode node = new RadTreeNode();
        //        node.Text = org.ContactName;
        //        node.Value = org.ContactRecno.ToString();
        //        node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;

        //        e.Node.Nodes.Add(node);
        //    }

        //    // Match already selected nodes
        //    List<FOrganisation> listSelectedOrganisations = new ClassCodeManager().GetOrgUnitsByClassCode(mCodeID, mFacetID);

        //    foreach (FOrganisation org in listSelectedOrganisations)
        //    {
        //        RadTreeNode node = treeOrganisations.FindNodeByValue(org.ContactRecno.ToString());

        //        if (node != null)
        //            node.Checked = true;
        //    }

        //}

        //private List<int> GetOrgUnitsFromParent(int iOrgUnit)
        //{
        //    List<int> OrgList = new List<int>();
        //    List<FOrganisation> SubList = new ContactManager().GetOrganisationsByParent(iOrgUnit);

        //    foreach (FOrganisation org in SubList)
        //    {
        //        OrgList.Add(org.ContactRecno);
        //        OrgList.AddRange(GetOrgUnitsFromParent(org.ContactRecno));
        //    }

        //    return OrgList;

        //}

        //private List<int> GetSelectedOrgUnitsFromParent(int iOrgUnit)
        //{
        //    List<int> OrgList = new List<int>();
        //    List<FOrganisation> SubList = new ContactManager().GetOrganisationsByParent(iOrgUnit);

        //    foreach (FOrganisation org in SubList)
        //    {
        //        OrgList.Add(org.ContactRecno);
        //        OrgList.AddRange(GetOrgUnitsFromParent(org.ContactRecno));
        //    }

        //    return OrgList;

        //}

        //void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    // Get query values
        //    GetQuery();

        //    // Delete all nodes in table
        //    foreach (RadTreeNode node in treeOrganisations.GetAllNodes())
        //    {
        //        string sOrgUnitID = node.Value;
        //        bool bSuccess = new ClassCodeManager().DeleteByClassCode(mCodeID, mFacetID, sOrgUnitID);
        //    }

        //    // Insert all checked orgunits
        //    foreach (RadTreeNode node in treeOrganisations.CheckedNodes)
        //    {
        //        string sOrgUnitID = node.Value;
        //        bool bSuccess = new ClassCodeManager().InsertByClassCode(mCodeID, mFacetID, sOrgUnitID);

        //        /*
        //        if (node.Nodes.Count == 0)
        //        {
        //            List<int> SubList = GetOrgUnitsFromParent(Convert.ToInt32(sOrgUnitID));

        //            foreach (int element in SubList)
        //                new ClassCodeManager().InsertByClassCode(mCodeID, mFacetID, element.ToString());
        //        }*/
        //    }

        //    // Show updated message
        //    lblUpdated.Visible = true;

        //    // Re-bind data
        //    BindData();

        //}

        //private void BuildTree(RadTreeNodeCollection ParentNode, List<FOrganisation> OrganisationsList, List<FOrganisation> SelectedOrganisationsList)
        //{
        //    foreach (FOrganisation org in OrganisationsList)
        //    {
        //        RadTreeNode node = new RadTreeNode();
        //        node.Text = org.ContactName;
        //        node.Value = org.ContactRecno.ToString();
        //        node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
        //        node.Checked = false;

        //        ParentNode.Add(node);

        //        foreach (FOrganisation selected in SelectedOrganisationsList)
        //        {
        //            if (org.ContactRecno == selected.ContactRecno)
        //            {
        //                List<FOrganisation> SubOrganisationsList = new ContactManager().GetOrganisationsByParent(org.ContactRecno);
        //                BuildTree(node.Nodes, SubOrganisationsList, SelectedOrganisationsList);
        //                node.ExpandMode = TreeNodeExpandMode.ClientSide;
        //                node.Checked = true;
        //                break;
        //            }
        //        }
        //    }
        //}

        //private void BindData()
        //{
        //    // Reset tree
        //    treeOrganisations.Nodes.Clear();

        //    // Get top level organisations
        //    List<FOrganisation> listOrganisations = new ContactManager().GetOrganisationsByParent(null);

        //    // Match already selected nodes
        //    List<FOrganisation> listSelectedOrganisations = new ClassCodeManager().GetOrgUnitsByClassCode(mCodeID, mFacetID);

        //    // Build organisations tree
        //    BuildTree(treeOrganisations.Nodes, listOrganisations, listSelectedOrganisations);

        //    // Expand all nodes
        //    treeOrganisations.ExpandAllNodes();
        //}

        //private void CheckRecursive(List<FOrganisation> orgs)
        //{
        //    foreach (FOrganisation org in orgs)
        //    {
        //        RadTreeNode node = treeOrganisations.FindNodeByValue(org.ContactRecno.ToString());

        //        if (node != null)
        //            node.Checked = true;
        //    }
        //}

        private void GetQuery()
        {
            // GET QUERY VALUES
            if (HttpContext.Current.Request.QueryString["code"] != null)
                mCode = HttpContext.Current.Request.QueryString["code"].ToString();

            if (HttpContext.Current.Request.QueryString["facet"] != null)
                mFacet = HttpContext.Current.Request.QueryString["facet"].ToString();

            if (HttpContext.Current.Request.QueryString["codeID"] != null)
                mCodeID = HttpContext.Current.Request.QueryString["codeID"].ToString();

            if (HttpContext.Current.Request.QueryString["facetID"] != null)
                mFacetID = HttpContext.Current.Request.QueryString["facetID"].ToString();
        }
    }
}