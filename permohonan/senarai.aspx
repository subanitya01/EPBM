<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai.aspx.cs" Inherits="EPBM.Permohonan.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">SENARAI <strong>PERMOHONAN</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="input-group">
				<select class="form-select">
					<option>SEMUA KOLUM</option>
					<option>TAJUK</option>
					<option>JABATAN</option>
					<option>STATUS</option>
				</select>
				<input type="text" class="form-control w-25" placeholder="Carian...">
				<button class="btn btn-primary" type="button"><i class="align-middle" data-feather="search"></i></button>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-body table-responsive">
			<table class="table table-bordered table-striped table-hover">
			  <thead>
				<tr>
				  <th scope="col">#</th>
				  <th scope="col">TAJUK</th>
				  <th scope="col">JABATAN</th>
				  <th scope="col">HARGA INDIKATIF (RM)</th>
				  <th scope="col">TARIKH SAHLAKU</th>
				  <th scope="col">STATUS</th>
				  <th scope="col"></th>
				</tr>
			  </thead>
			  <tbody>
				<tr>
				  <th scope="row">1</th>
				  <td class="">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</td>
				  <td class="text-end">3,456,789.00</td>
				  <td class="text-nowrap">04 OKT 2023</td>
				  <td class="text-center"><span class="badge text-bg-primary">BELUM DISAHKAN</span></td>
				  <td class="table-action">
						<a href="/permohonan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/permohonan/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
						<a href="#" data-bs-toggle="modal" data-bs-target="#deleteModal" class="text-danger" title="Hapus"><i class="align-middle" data-feather="trash"></i></a>
					</td>
				</tr>
				<tr>
				  <th scope="row">2</th>
				  <td class="">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</td>
				  <td class="text-end">3,456,789.00</td>
				  <td class="text-nowrap">04 Okt 2023</td>
				  <td class="text-center"><span class="badge text-bg-warning">TIDAK DISAHKAN</span></td>
				  <td class="table-action">
						<a href="/permohonan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/permohonan/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
						<a href="#" data-bs-toggle="modal" data-bs-target="#deleteModal" class="text-danger" title="Hapus"><i class="align-middle" data-feather="trash"></i></a>
					</td>
				</tr>
				<tr>
				  <th scope="row">3</th>
				  <td class="">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</td>
				  <td class="text-end">3,456,789.00</td>
				  <td class="text-nowrap">04 Okt 2023</td>
				  <td class="text-center"><span class="badge text-bg-success">DISAHKAN</span></td>
				  <td class="table-action">
						<a href="/permohonan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="#" data-bs-toggle="modal" data-bs-target="#meetingModal" class="text-info" title="Bawa Ke Mesyuarat"><i class="align-middle" data-feather="send"></i></a>
					</td>
				</tr>
				<tr>
				  <th scope="row">4</th>
				  <td class="">PEROLEHAN PEMBAHARUAN LANGGANAN APLIKASI ZOOM BAGI VIRTUAL MEETING KEMENTERIAN SUMBER ASLI, ALAM SEKITAR DAN PERUBAHAN IKLIM</td>
				  <td>BAHAGIAN PENGURUSAN MAKLUMAT</td>
				  <td class="text-end">8,530.56</td>
				  <td class="text-nowrap">22 Nov 2023</td>
				  <td class="text-center"><span class="badge text-bg-info">BAWA KE MESYUARAT</span></td>
				  <td class="table-action">
						<a href="/permohonan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/keputusan/edit.aspx" class="text-success" title="Keputusan"><i class="align-middle" data-feather="inbox"></i></a>
					</td>
				</tr>
			  </tbody>
			</table>
			<nav aria-label="Page navigation example">
			  <ul class="pagination justify-content-end">
				<li class="page-item">
				  <a class="page-link" href="#" aria-label="Previous">
					<span aria-hidden="true">&laquo;</span>
				  </a>
				</li>
				<li class="page-item active"><a class="page-link" href="#">1</a></li>
				<li class="page-item"><a class="page-link" href="#">2</a></li>
				<li class="page-item"><a class="page-link" href="#">3</a></li>
				<li class="page-item">
				  <a class="page-link" href="#" aria-label="Next">
					<span aria-hidden="true">&raquo;</span>
				  </a>
				</li>
			  </ul>
			</nav>
		</div>
	</div>
	<div class="modal fade" id="deleteModal" tabindex="-1" aria-modal="true" role="dialog">
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
