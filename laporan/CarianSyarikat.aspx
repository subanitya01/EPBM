<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarianSyarikat.aspx.cs" Inherits="EPBM.laporan.CarianSyarikat" %>
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


<h1 class="h3 mb-3">CARIAN SYARIKAT BERJAYA</h1>	
<asp:ScriptManager ID="ScriptManager1"  EnablePageMethods="true"   EnablePartialRendering="true" runat="server">  </asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
   <ContentTemplate>  
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
						 <label class="control-label">Pilih Nama Syarikat<span class="text-danger">*</span></label>       <br />                 
                         <asp:DropDownList ID="ddlSyarikat" class="form-select mb-3" CssClass="chosen-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSyarikat_SelectedIndexChanged"></asp:DropDownList><br /><br>
					 <asp:Button ID="btncari" runat="server" Text="Cari" OnClick="btnCari_Click" CssClass="btn btn-primary" />
                    </div>
				</div>
			</div>
		</div>
	</div>
       <asp:Panel ID="PanelGrid" Visible="false" runat="server">
           
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
                           <asp:BoundField DataField="NamaJabatan" HeaderText="KEMEN/JAB" SortExpression="Jabatan">
                               <ItemStyle Width="20%" Wrap="true" />
                           </asp:BoundField>
                           <asp:BoundField DataField="Nama_JPerolehan" HeaderText="JENIS PEROLEHAN" SortExpression="Jenis Perolehan">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                            <asp:BoundField DataField="Harga" HeaderText="HARGA" DataFormatString= "RM {0:n}" SortExpression="Harga">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                             <asp:BoundField DataField="TarikhSuratSetujuTerima" HeaderText="TKH SST" DataFormatString="{0:dd/MM/yyyy}" SortExpression="TarikhSuratSetujuTerima">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                           <asp:BoundField DataField="Tempoh" HeaderText="TEMPOH" SortExpression="Tempoh">
                               <ItemStyle Wrap="true" />
                           </asp:BoundField>
                           <%--<asp:BoundField DataField="Status_Permohonan" HeaderText="Status" SortExpression="Status">
                                                <ItemStyle  Wrap="true" />
                                            </asp:BoundField>--%>
                           <asp:TemplateField HeaderText="Status">
                               <ItemStyle Width="0.5%" Wrap="true" />
                               <ItemTemplate>
                                   <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StatusKeputusan") %>'></asp:Label>
                                   <asp:Label ID="lblIDStatus" Visible="false" runat="server" Text='<%# Eval("Id_StatusKeputusan") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                        <%--   <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
                               <ItemStyle Width="0.5%" Wrap="true" />
                               <ItemTemplate>
                                   <a href="papar-pengesahan.aspx?id=<%# Eval("Id") %>">
                                       <img src="/image/check.png" title=""></a>
                               </ItemTemplate>
                           </asp:TemplateField>--%>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
