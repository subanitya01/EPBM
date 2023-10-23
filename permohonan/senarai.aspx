<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="senarai.aspx.cs" Inherits="EPBM.Permohonan.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<link href="../assets/css/gridview.css" rel="stylesheet" />


<style>
img {
padding-top: 10px;
width: 22px;
height: 27px;
}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<%-- <ajaxToolkit:ToolkitScriptManager ID="tsmPage" runat="server" ScriptMode="Release" />--%>
<h1 class="h3 mb-3">SENARAI <strong>PEROLEHAN</strong></h1>

<div class="card">
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server" defaultbutton="btnSearch">
				<div class="input-group">
					<asp:DropDownList ID="listSearchCol" CssClass="form-select" runat="server" >  
						<asp:ListItem Value="">SEMUA KOLUM</asp:ListItem>  
						<%--<asp:ListItem>JABATAN</asp:ListItem>  
						<asp:ListItem>TAJUK</asp:ListItem>  
						<asp:ListItem>STATUS</asp:ListItem> --%> 
					</asp:DropDownList>
					<asp:TextBox ID="txtSearch" CssClass="form-control w-25" placeholder="Carian..." runat="server"></asp:TextBox>
					<asp:LinkButton ID="btnSearch" CssClass="btn btn-primary" runat="server" OnClick="BtnSearch_Click" CausesValidation="False"><i class="align-middle" data-feather="search"></i></asp:LinkButton>
					
				
				</div>
			</asp:Panel>
		</div>
	</div>
<asp:ScriptManager ID="ScriptManager1"  EnablePageMethods="true"   EnablePartialRendering="true" runat="server">  </asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
   <ContentTemplate>

	<div class="card">
		<div class="card-body table-responsive">

		
			<div ID="SORT" runat="server" Visible="true" class="row input-group-sm justify-content-between">
				<div   class ="col-sm-6 col-md-5 mb-3">
					<%--<label>SUSUNAN BERDASARKAN: <asp:Label ID="lblSortRecord" runat="server" /></label>--%>
					<label>SUSUNAN BERDASARKAN:
                               <asp:Label ID="lblSortRecord" runat="server" /></label>
                               <asp:Label ID="lblSortColumn" runat="server" Visible="false" />
                               <asp:Label ID="lblSortDirection" runat="server" Visible="false" />
                               <asp:Label ID="lblIcon" runat="server" Visible="false" />
				</div>
				<div class="col-sm-3 col-md-2 mb-3 text-end">
					
				</div>
			</div>
			<table class="table table-bordered table-striped table-hover">
                <asp:GridView ID="Senarai" runat="server" AutoGenerateColumns="False" EmptyDataText="Tiada Rekod Dijumpai." ShowHeaderWhenEmpty="True" OnPageIndexChanging="Senarai_PageIndexChanging" OnSorting="Senarai_Sorting" AllowSorting="true" CssClass="table table-striped table-bordered table-hover Grid" OnDataBound="Senarai_DataBound" OnRowDataBound="OnRowDataBound" AllowPaging="True">
                    <PagerSettings Mode="Numeric" Position="Bottom" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false" SortExpression="ID">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle Width="0.5%" Wrap="true" />
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TAJUK" SortExpression="Tajuk">
                            <ItemStyle Width="50%" Wrap="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblTajukUtama" runat="server" Text='<%# Eval("Tajuk") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NamaJabatan" HeaderText="KEMENTERIAN /JABATAN" SortExpression="NamaJabatan">
                            <ItemStyle Width="20%" Wrap="true" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ShortName" Visible="false">
                            <ItemStyle Width="0.5%" Wrap="true" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("ShortName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Harga" HeaderText="HARGA" DataFormatString="RM {0:n}" SortExpression="Harga">
                            <ItemStyle Width="12%" Wrap="true" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="STATUS" Visible="true" SortExpression="Status_Permohonan">
                            <ItemStyle Width="0.5%" Wrap="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status_Permohonan") %>'></asp:Label>
                                <asp:Label ID="lblIDStatus" Visible="false" runat="server" Text='<%# Eval("IdStatusPermohonan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TINDAKAN">
                            <ItemStyle Width="9%" Wrap="true" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLinkPapar" runat="server" NavigateUrl='<%# Eval("Id", "papar.aspx?ID={0}") %>' ImageUrl="~/image/View.png" title="Papar"></asp:HyperLink>
                                <asp:HyperLink ID="HyperLinkEdit" runat="server" Visible="false" NavigateUrl='<%# Eval("Id", "edit.aspx?ID={0}") %>' ImageUrl="~/image/Edit.png" title="Kemaskini"></asp:HyperLink>
<%--                                <asp:HyperLink ID="HyperLinkMaju" runat="server" Visible="false" NavigateUrl='<%# Eval("Id", "MajuMesyuarat.aspx?ID={0}") %>' ImageUrl="~/image/Maju.png" Style="text-align: center" title="Maju Mesyuarat"></asp:HyperLink>--%>
                                <asp:ImageButton ID="btnhapus" runat="server" Visible="false" ImageUrl="~/image/delete.png" OnClick="btn_hapus_Click" ImageAlign="middle" Width="18" Height="18" title="Delete" />

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>

			</table>
			<nav aria-label="Page navigation example">
			
			</nav>
		</div>
	<asp:TextBox ID="ID" runat="server" Style="display: none;" type="text"></asp:TextBox>		
	</div>	   
	<div class="modal fade" id="deleteModal" tabindex="-1"  aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">				
				<div class="modal-header bg-danger">
					<h5 class="modal-title text-white text-truncate"><asp:Label ID="lblTajukUtama" runat="server"></asp:Label> </h5>				    
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<br />	
					<p class="mb-0">Anda pasti untuk menghapuskan permohonan ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
				    <asp:LinkButton ID="btnhapus" runat="server" OnClick="btnSahHapus_Click" Text="HAPUS" class="btn btn-danger" > </asp:LinkButton>
				</div>
			</div>
		</div>
		
	</div>


</ContentTemplate>    </asp:UpdatePanel> 


<script type="text/javascript">
    
	function opendeleteModal() {
        var myModal = new bootstrap.Modal(document.getElementById('deleteModal'), { keyboard: false });
        myModal.show();
    }
</script>

</asp:Content>
