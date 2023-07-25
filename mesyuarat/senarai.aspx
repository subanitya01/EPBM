<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai.aspx.cs" Inherits="EPBM.mesyuarat.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">SENARAI <strong>MESYUARAT</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="input-group">
				<select class="form-select">
					<option>SEMUA KOLUM</option>
					<option>JENIS</option>
					<option>BILANGAN</option>
					<option>TARIKH</option>
					<option>PENGERUSI</option>
				</select>
				<input type="text" class="form-control w-25" placeholder="Carian...">
				<button class="btn btn-primary" type="button"><i class="align-middle" data-feather="search"></i></button>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-body">
			<table class="table table-bordered table-striped table-hover">
			  <thead>
				<tr>
				  <th scope="col">#</th>
				  <th scope="col">JENIS</th>
				  <th scope="col">BILANGAN</th>
				  <th scope="col">TARIKH</th>
				  <th scope="col">PENGERUSI</th>
				  <th scope="col">JUMLAH KELULUSAN</th>
				  <th scope="col"></th>
				</tr>
			  </thead>
			  <tbody>
				<tr>
				  <th scope="row">1</th>
				  <td class="">MLP</td>
				  <td>1/2023</td>
				  <td class="text-nowrap">04 OKT 2023</td>
				  <td>MOHD NORHISHAM BIN MUSA</td>
				  <td class="text-center">0/5</td>
				  <td class="table-action">
						<a href="/mesyuarat/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/mesyuarat/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
						<a href="/mesyuarat/senarai-keputusan.aspx" class="text-success" title="Keputusan"><i class="align-middle" data-feather="inbox"></i></a>
						<a href="#" data-bs-toggle="modal" data-bs-target="#deleteModal" class="text-danger" title="Hapus"><i class="align-middle" data-feather="trash"></i></a>
					</td>
				</tr>
				<tr>
				  <th scope="row">2</th>
				  <td class="">JKSH</td>
				  <td>2/2023</td>
				  <td class="text-nowrap">22 NOV 2023</td>
				  <td>TAJUDDIN TAN BIN ABDULLAH</td>
				  <td class="text-center">7/10</td>
				  <td class="table-action">
						<a href="/mesyuarat/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/mesyuarat/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
						<a href="/mesyuarat/senarai-keputusan.aspx" class="text-success" title="Keputusan"><i class="align-middle" data-feather="inbox"></i></a>
						<a href="#" data-bs-toggle="modal" data-bs-target="#deleteModal" class="text-danger" title="Hapus"><i class="align-middle" data-feather="trash"></i></a>
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
					<h5 class="modal-title text-white text-truncate">MESYUARAT MLP Bil. 1/2023</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="mb-0">Anda pasti untuk menghapuskan mesyuarat ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-danger" data-bs-dismiss="modal">Hapus</button>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
