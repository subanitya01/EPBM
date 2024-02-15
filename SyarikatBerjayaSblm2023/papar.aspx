<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="papar.aspx.cs" Inherits="EPBM.SyarikatBerjayaSblm2023.papar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<%--	
	<h1 class="h3 mb-4 text-truncate">PERMOHONAN: <strong> <asp:Label ID="lblTajukUtama" runat="server" ></asp:Label></strong></h1>
	<div class="card">
	--%>


		<div class="card-body">
	<div class="btn-group btn-group-sm mb-3 float-end" role="group">
			<a href="/SyarikatBerjayaSblm2023/CarianSyarikat.aspx" class="btn btn-secondary" >Back</a>			
			</div>
			<table class="table table-bordered table-hover">
			  <tbody>			
				 <tr>
				  <th scope="row" class="align-middle bg-secondary text-white w-25">TAJUK</th>
				  <td class=""> <strong><asp:Label ID="lblTajuk" runat="server" ></asp:Label></strong></td>
				</tr>

				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">JENIS PEROLEHAN/KONTRAK</th>
				  <td class=""><asp:Label ID="lblJenisPerolehan_Kontrak" runat="server" ></asp:Label><br />					  
                             <asp:Label ID="lblLainLainJPerolehan" runat="server" Visible="false"></asp:Label>
                   
				</tr>				  	 
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">JABATAN</th>
				  <td class=""><asp:Label ID="lblJabatan" runat="server" ></asp:Label></td>
				 
				</tr>

				<asp:Panel ID="PnlBahagian" runat="server" Visible="false">
				  <tr>
				  <th scope="row" class="align-middle bg-secondary text-white">BAHAGIAN</th>
				  <td class=""><asp:Label ID="lblBahagian" runat="server" ></asp:Label></td>
				 
				</tr>
				</asp:Panel>

				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">HARGA INDIKATIF / NILAI KONTRAK</th>
				  <td class="">RM <asp:Label ID="lblHargaIndikatif" runat="server" ></asp:Label></td>
				</tr>
 
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">Tahun Lantikan </th>
				  <td class=""><asp:Label ID="lblTahunLantiakan" runat="server" ></asp:Label></td>
				</tr>
                  <tr>
                      <th scope="row" class="align-middle bg-secondary text-white">TARIKH SAHLAKU TENDER/KONTRAK</th>
                      <td class="">
                          <asp:Label ID="lblTempoh" runat="server"></asp:Label></td>
                  </tr>
                  <tr>
                      <th scope="row" class="align-middle bg-secondary text-white">TARIKH SAHLAKU TENDER/KONTRAK</th>
                      <td class="">
                          <asp:Label ID="lblNamaSyarikat" runat="server"></asp:Label></td>
                  </tr>

				  <tr>
				  <th scope="row" class="align-middle bg-secondary text-white">Catatan</th>
				  <td class=""><asp:Label ID="lblCatatan" runat="server" ></asp:Label></td>
				</tr>

			  </tbody>
			</table>
		</div>
	<%--</div>--%>
	<div class="modal fade" id="confirmationModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-danger">
					<h5 class="modal-title text-white text-truncate">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="mb-0">Anda pasti untuk menghapuskan permohonan ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-danger" data-bs-dismiss="modal">Hapus</button>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="meetingModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-info">
					<h5 class="modal-title text-white text-truncate">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="">Anda pasti untuk bawa permohonan ini ke mesyuarat?</p>
					
					<div>
						<select class="form-select mb-3" required>
						  <option>SILA PILIH MESYUARAT</option>
						  <option>MLP BIL. 1/2023</option>
						  <option>JKSH BIL. 1/2023</option>
						</select>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-info" data-bs-dismiss="modal">Teruskan</button>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
