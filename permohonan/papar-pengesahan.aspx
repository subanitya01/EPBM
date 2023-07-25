<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="papar-pengesahan.aspx.cs" Inherits="EPBM.permohonan.papar_pengesahan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-4 text-truncate">PERMOHONAN: <strong>PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
				<a href="#" data-bs-toggle="modal" data-bs-target="#approveModal" class="btn btn-primary"><i class="mt-n1" data-feather="check"></i> SAHKAN</a>
				<a href="#" data-bs-toggle="modal" data-bs-target="#rejectModal" class="btn btn-warning"><i class="mt-n1" data-feather="x"></i> KEMBALIKAN</a>
			</div>
			<table class="table table-bordered table-hover">
			  <tbody>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TAJUK</th>
				  <td class="">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PERTIMBANGAN</th>
				  <td class="">PENTADBIRAN KONTRAK</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">KAEDAH PEROLEHAN</th>
				  <td class="">RUNDINGAN TERUS (PK 2.4)</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PEROLEHAN/KONTRAK</th>
				  <td class="">PERKHIDMATAN</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JABATAN</th>
				  <td class="">JABATAN UKUR DAN PEMETAAN MALAYSIA</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">HARGA INDIKATIF / NILAI KONTRAK</th>
				  <td class="">RM 3,456,789.00</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">SUMBER PERUNTUKAN</th>
				  <td class="">BELANJA MENGURUS</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">PBM MUKTAMAD</th>
				  <td class="">MOF</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH SAHLAKU TENDER/KONTRAK</th>
				  <td class="">04 Disember 2023</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH TERIMA</th>
				  <td class="">04 Oktober 2023</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN</th>
				  <td class="">YA</td>
				</tr>
			  </tbody>
			</table>
		</div>
	</div>
	<div class="modal fade" id="approveModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-primary">
					<h5 class="modal-title text-white text-truncate">SAHKAN PERMOHONAN</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="mb-0">Anda pasti untuk mengesahkan permohonan ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-primary" data-bs-dismiss="modal">SAHKAN</button>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="rejectModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-warning">
					<h5 class="modal-title text-white text-truncate">KEMBALIKAN PERMOHONAN</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="">Anda pasti untuk bawa kembalikan permohonan ini untuk pengemaskinian?</p>
					
					<div>
						<textarea class="form-control mb-3" rows="4" placeholder="CATATAN"></textarea>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-warning" data-bs-dismiss="modal">KEMBALIKAN</button>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
