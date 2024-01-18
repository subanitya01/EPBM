<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carian.aspx.cs" Inherits="EPBM.laporan.Carian" %>
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
<asp:ScriptManager ID="ScriptManager1"  EnablePageMethods="true"   EnablePartialRendering="true" runat="server">  </asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
   <ContentTemplate>  
       <div id="MessageAlert" runat="server" visible="false" class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
           <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
           <div class="alert-icon me-3">
               <i class="mt-n1" data-feather="bell"></i>
           </div>
           <div class="alert-message">
               Sila Semak Tarikh Mula dan Tarik Akhir <br>
           </div>

       </div>
<h1 class="h3 mb-3">CARIAN <strong>LAPORAN</strong></h1>
	<div id="advancedSearch" class="card mt-3">
		<div class="row card-body">

				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">JENIS LAPORAN</label>
					 <asp:Panel ID="pnlFilterLaporan" runat="server">             
                         <asp:DropDownList ID="ddlFilterLaporan" runat="server" class="form-select" AutoPostBack="true" CssClass="chosen-select" OnSelectedIndexChanged="ddlFilterLaporan_SelectedIndexChanged">
                             <asp:ListItem Value="0">Kementerian dan Jabatan</asp:ListItem>
                             <asp:ListItem Value="1">Kementerian</asp:ListItem>
                             <asp:ListItem Value="2">Jabatan</asp:ListItem>
                         </asp:DropDownList>
                       </asp:Panel>
                    <asp:Panel ID="pnlFilterJabatan" runat="server"> <br />
                        <label class="control-label">Jabatan<span class="text-danger">*</span></label>
                        <br />
                        <asp:DropDownList ID="ddlFilterJabatan" class="form-select" CssClass="chosen-select" runat="server"></asp:DropDownList><br />
                        <br>
                    </asp:Panel>
				</div>
				<div class="col-12 col-lg-6 mb-3">
                    <label class="control-label">SUMBER PERUNTUKAN </label>						
                   <asp:DropDownList ID="ddlSumberPeruntukan" class="form-select" runat="server" CssClass="chosen-select" DataTextField="SumberPeruntukan" AutoPostBack="True" OnSelectedIndexChanged="ddlSumberPeruntukan_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Panel ID="PnlSumberPeruntukan" runat="server" Visible="false">
                        <asp:TextBox ID="txtSumberPeruntukan" runat="server" class="form-control" placeholder="Sila Masukan lain-lain Sumber Peruntukan"></asp:TextBox>
                        <br />
                    </asp:Panel>
				</div>
			    <div class="col-12 col-lg-6 mb-3">
					<label class="control-label">TARIKH MULA </label>					 
				    <asp:TextBox ID="txttkhmula" type="date" runat="server" autocomplete="off" required="required" onkeydown="return false" class="form-control mb-3" AutoPostBack="true" placeholder="TARIKH MULA"></asp:TextBox>
					<label class="control-label">TARIKH AKHIR</label>
				    <asp:TextBox ID="txttkhakhir" runat="server" type="date"  autocomplete="off" required="required" onkeydown="return false" class="form-control mb-3" AutoPostBack="true" placeholder="TARIKH AKHIR"></asp:TextBox>					 
				</div>

				<div class="col-12 col-lg-6 mb-3">
                    <label class="control-label">JENIS PEROLEHAN / KONTRAK <span class="text-danger">*</span></label>
                    <asp:DropDownList ID="ddlJenisPerolehan" class="form-select" runat="server" DataTextField="JenisPerolehan" CssClass="chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlJenisPerolehan_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Panel ID="PnlJenisPerolehan" runat="server" Visible="false">
                        <asp:TextBox ID="txtJenisPerolehan" runat="server" class="form-control" placeholder="Sila Masukan lain-lain Jenis Perolehan"></asp:TextBox>
                        <br />
                    </asp:Panel>
				</div>			 				 
				<div class="col-12 text-end">
					<asp:Button ID="btncari" runat="server" Text="Cari" OnClick="btnCari_Click" CssClass="btn btn-primary" />
				</div>
		</div>
	</div>

       <%--<div class="card">
           <div class="card-body">
               <br>
               
           </div>

       </div>--%>
       <asp:Panel ID="PanelGrid" Visible="false" runat="server">          
           <div class="card">
               <div class="card-body table-responsive">

                   <div id="SORT" runat="server" visible="true" class="row input-group-sm justify-content-between">
                       <div class="col-sm-6 col-md-8 mb-3">
                           <label>SUSUNAN BERDASARKAN:
                               <asp:Label ID="lblSortRecord" runat="server" /></label>
                               <asp:Label ID="lblSortColumn" runat="server" Visible="false" />
                               <asp:Label ID="lblSortDirection" runat="server" Visible="false" />
                               <asp:Label ID="lblIcon" runat="server" Visible="false" />
                       </div>
                       <div class="col-sm-3 col-md-2 mb-3 text-end">
                       </div>
                   </div>

                   <asp:GridView ID="Senarai" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="Senarai_PageIndexChanging" CssClass="table table-striped table-bordered table-hover Grid" OnDataBound="Senarai_DataBound" EmptyDataText="Tiada Rekod Carian." ShowHeaderWhenEmpty="True" OnSorting="Senarai_Sorting" AllowSorting="true" AllowPaging="True">
                       <PagerSettings Mode="Numeric" Position="Bottom" />
                       <Columns>
                           <asp:TemplateField HeaderText="ID" Visible="false">
                               <ItemTemplate>
                                   <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="No.">
                               <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="Tajuk" HeaderText="TAJUK" SortExpression="Tajuk">
                               <ItemStyle Width="45%" Wrap="true" />
                           </asp:BoundField>
                           <asp:BoundField DataField="NamaJabatan" HeaderText="KEMENTERIAN / JABATAN" SortExpression="NamaJabatan">
                               <ItemStyle Width="20%" Wrap="true" />
                           </asp:BoundField>
                           <asp:BoundField DataField="Nama_JPerolehan" HeaderText="JENIS PEROLEHAN" SortExpression="Nama_JPerolehan">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField >                          
                            <asp:BoundField DataField="Harga" HeaderText="HARGA"  FooterText="Total" DataFormatString= "RM {0:#,##0.00}" SortExpression="Harga">
                               <ItemStyle Wrap="true" />
                            </asp:BoundField>                            
                             <asp:BoundField DataField="TarikhSuratSetujuTerima" HeaderText="TKH SST" DataFormatString="{0:dd/MM/yyyy}" SortExpression="TarikhSuratSetujuTerima">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>           
                           <asp:BoundField DataField="PBM" HeaderText="MUKTAMAD" SortExpression="PBM">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField> 
                           <asp:TemplateField HeaderText="STATUS" SortExpression="StatusKeputusan" >
                               <ItemStyle Width="0.5%" Wrap="true" />
                               <ItemTemplate>
                                   <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StatusKeputusan") %>'></asp:Label>
                                   <asp:Label ID="lblIDStatus" Visible="false" runat="server" Text='<%# Eval("Id_StatusKeputusan") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>                   
                   </asp:GridView>

                   <asp:Panel ID="Panel1" Visible="false" runat="server">
                        <div class="btn-group btn-group-sm mb-3 float-end" role="group">                           
                           <div class="form-group">
                              <label class="col-sm-8 ">
                           </div>
                       </div>

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
                           Jumlah Peruntukan  : RM <asp:Label ID="lbltotal_RepTahun2" runat="server" /></label>  <br />  
                           Jumlah Rekod :
                      <asp:Label ID="total_RepTahun" runat="server" />
                       </label>
                   </div>
       </asp:Panel>
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
    $(document).ready(function () {
        $(".chosen-select").chosen({ search_contains: true, allow_single_deselect: true });
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        $(".chosen-select").chosen({ search_contains: true, allow_single_deselect: true });
    });
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
