<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarianSyarikat.aspx.cs" Inherits="EPBM.SyarikatBerjayaSblm2023.CarianSyarikat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

<link href="../assets/css/gridview.css" rel="stylesheet" />
<script src="../assets/js/jquery.min.js"></script>

<link href="../assets/css/chosen.min.css" rel="stylesheet"/>	
<script src="../assets/js/jquery.chosen.min.js"></script>
<link href="../assets/css/chosen.css" rel="stylesheet" />

<style>
	img {
padding-top: 10px;
width: 22px;
height: 27px;
}

</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
     
    <span class="btn-group btn-group-sm float-end" role="group">
        <a class="btn btn-success" href="daftar.aspx" role="button" aria-expanded="false" aria-controls="add" title="Tambah Syarikat Berjaya"><i class="align-middle" data-feather="plus"></i>Tambah Syarikat Berjaya</a>
    </span>

<h1 class="h3 mb-3">CARIAN SYARIKAT BERJAYA</h1>	

<asp:ScriptManager ID="ScriptManager1"  EnablePageMethods="true"   EnablePartialRendering="true" runat="server">  </asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
   <ContentTemplate>  
    
    	<div id="addMeeting" class="card collapse">
		<div class="card-header pb-0">
			<h5 class="card-title">DAFTAR MESYUARAT BARU</h5>
		</div>
	 
	</div>       

    <div id="MessageAlert" runat="server" Visible="false" class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
				<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
				<div class="alert-icon me-3">
					<i class="mt-n1" data-feather="bell"></i>
				</div>
				<div class="alert-message">
					 Sila Pilih Nama syarikat!<br>
								 
				</div>
         
			</div>
       
       
       <div class="card">
		<div class="card-body">
			<div class="row">				
				<div class="col-12 col-lg-6 success">
                    <div>
                        <label class="control-label">TARIKH MULA </label>
                        <asp:TextBox ID="txttkhmula" type="date" runat="server" autocomplete="off" onkeydown="return false" class="form-control mb-3" AutoPostBack="true" placeholder="TARIKH MULA"></asp:TextBox>
                        <label class="control-label">TARIKH AKHIR</label>
                        <asp:TextBox ID="txttkhakhir" runat="server" type="date" autocomplete="off" onkeydown="return false" class="form-control mb-3" AutoPostBack="true" placeholder="TARIKH AKHIR"></asp:TextBox>
                        <label class="control-label">Pilih Nama Syarikat<span></span></label>
                        <br />
                        <asp:DropDownList ID="ddlSyarikat" class="form-select mb-3" CssClass="chosen-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSyarikat_SelectedIndexChanged"></asp:DropDownList><br />
                        <br>
                        <asp:HyperLink ID="btnReset" NavigateUrl="/SyarikatBerjayaSblm2023/CarianSyarikat.aspx" runat="server" CssClass="btn btn-outline-danger">RESET</asp:HyperLink>
                        <asp:Button ID="btncari" runat="server" Text="Cari" OnClick="btnCari_Click" CssClass="btn btn-primary" />

                    </div>
				</div>

                <div class="col-12 col-lg-6 mb-3">
                    
                </div>
			</div>
		</div>
	</div>
       <asp:Panel ID="PanelGrid" Visible="true" runat="server">
           
           <div class="card">
               <div class="card-body table-responsive">
                   <asp:GridView ID="Senarai" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="Senarai_PageIndexChanging" CssClass="table table-striped table-bordered table-hover Grid" OnDataBound="Senarai_DataBound" AllowPaging="True">
                       <PagerSettings Mode="Numeric" Position="Bottom" />
                       <Columns>
                           <asp:TemplateField HeaderText="ID" Visible="false">
                               <ItemTemplate>
                                   <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="NO.">
                               <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="Tajuk" HeaderText="TAJUK" SortExpression="Tajuk">
                               <ItemStyle Width="45%" Wrap="true" />
                           </asp:BoundField>
                           <%--<asp:BoundField DataField="NamaJabatan" HeaderText="KEMEN/JAB" SortExpression="Jabatan">
                               <ItemStyle Width="20%" Wrap="true" />
                           </asp:BoundField>--%>
                           <asp:BoundField DataField="NamaJabatan" HeaderText="KEMENTERIAN/ JABATAN " SortExpression="NamaJabatan">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                           <asp:BoundField DataField="Nama" HeaderText="JENIS PEROLEHAN" SortExpression="Jenis Perolehan">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                            <asp:BoundField DataField="Harga" HeaderText="HARGA" DataFormatString= "RM {0:n}" SortExpression="Harga">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                             <asp:BoundField DataField="TahunLantikan" HeaderText="TahunLantikan" DataFormatString="{0:dd/MM/yyyy}" SortExpression="TarikhSuratSetujuTerima">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                                 <asp:BoundField DataField="NamaSyarikat" HeaderText="NamaSyarikat" SortExpression="NamaSyarikat">
        <ItemStyle Wrap="true" />
    </asp:BoundField>
                           <asp:BoundField DataField="Tempoh" HeaderText="TEMPOH" SortExpression="Tempoh">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                                                   <asp:TemplateField HeaderText="TINDAKAN">
                            <ItemStyle Width="9%" Wrap="true" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLinkPapar" runat="server" NavigateUrl='<%# Eval("Id", "papar.aspx?ID={0}") %>' ImageUrl="~/image/View.png" title="Papar"></asp:HyperLink>
                                <asp:HyperLink ID="HyperLinkEdit" runat="server" NavigateUrl='<%# Eval("Id", "edit.aspx?ID={0}") %>' ImageUrl="~/image/Edit.png" title="Kemaskini"></asp:HyperLink>
                                <asp:ImageButton ID="btnhapus" runat="server" ImageUrl="~/image/delete.png" OnClick="btn_hapus_Click" ImageAlign="middle" Width="18" Height="18" title="Delete" />

                            </ItemTemplate>
                        </asp:TemplateField>
                           
                       </Columns>
                   </asp:GridView>


                   <asp:Panel ID="Panel1" Visible="false" runat="server">
                       <div class="btn-group btn-group-sm mb-3 float-end" role="group">

                           <div class="form-group">

                               <asp:Button ID="btnExcel" runat="server" OnClick="ExportToExcel" CssClass="btn btn-primary " Text="Export Excel" />
                               <asp:Button ID="btnPdf" runat="server" OnClick="ExportToPDF" CssClass="btn btn-primary" Text="Export PDF" />

                           </div>

                       </div>
                   </asp:Panel>


                   <asp:GridView ID="gvData" runat="server" Visible="false"></asp:GridView>
                   <br />
                   <div class="col-sm-8">
                       <label class="col-sm-8 ">
                           Jumlah Rekod :
                                                    <asp:Label ID="total_RepTahun" runat="server" />
                       </label>
                   </div>
       </asp:Panel>


       <asp:TextBox ID="ID" Style="display: none;" runat="server" ></asp:TextBox>


        </div>
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

	
</ContentTemplate>    

  <Triggers>
                    <asp:PostBackTrigger ControlID="btnExcel" />
                    <asp:PostBackTrigger ControlID="btnPdf" />
        
                </Triggers>




 </asp:UpdatePanel>   
    

<script type="text/javascript">
        $(document).ready(function () {
            $(".chosen-select").chosen({ search_contains: true, allow_single_deselect: true });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(".chosen-select").chosen({ search_contains: true, allow_single_deselect: true });
        });
</script>

<script type="text/javascript">

    function opendeleteModal() {
        var myModal = new bootstrap.Modal(document.getElementById('deleteModal'), { keyboard: false });
        myModal.show();
    }
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
