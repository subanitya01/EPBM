<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="papar.aspx.cs" Inherits="EPBM.permohonan.papar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-4 text-truncate">PERMOHONAN: <strong>PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
				<a href="/permohonan/edit.aspx" class="btn btn-secondary"><i class="mt-n1" data-feather="edit-2"></i > Edit</a>
				<a href="#" data-bs-toggle="modal" data-bs-target="#meetingModal" class="btn btn-info"><i class="mt-n1" data-feather="send"></i> Bawa Ke Mesyuarat</a>
				<a href="#" data-bs-toggle="modal" data-bs-target="#confirmationModal" class="btn btn-danger"><i class="mt-n1" data-feather="trash"></i> Hapus</a>
			</div>
			<div class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
				<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
				<div class="alert-icon me-3">
					<i class="mt-n1" data-feather="bell"></i>
				</div>
				<div class="alert-message">
					Terdapat kesilapan pada tarikh terima dan kaedah perolehan!<br>
					Sila semak dengan teliti<br>
					blablaablaaa
				</div>
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
				  <td class="">LAIN-LAIN (SUMBANGAN)</td>
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
