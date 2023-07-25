<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="papar.aspx.cs" Inherits="EPBM.mesyuarat.papar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-4 text-truncate">MESYUARAT: <strong>JKSH BIL. 1/2023</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
				<a href="/mesyuarat/edit.aspx" class="btn btn-secondary"><i class="mt-n1" data-feather="edit-2"></i > Edit</a>
				<a href="#" data-bs-toggle="modal" data-bs-target="#confirmationModal" class="btn btn-danger"><i class="mt-n1" data-feather="trash"></i> Hapus</a>
			</div>
			<table class="table table-bordered">
			  <tbody>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS</th>
				  <td class="">JKSH</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">BILANGAN</th>
				  <td class="">1/2023</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH</th>
				  <td class="">04 OKT 2023</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">PENGERUSI</th>
				  <td class="">MOHD NORHISHAM BIN MUSA</td>
				</tr>
				<tr>
				  <th scope="row" class="align-top bg-primary text-white">AHLI MESYUARAT</th>
				  <td class="">
					<ol class="list-group list-group-numbered">
						<li class="list-group-item">ZAENAP BINTI MOHAMAD</li>
						<li class="list-group-item">ZABIR BIN SARIP@SHARIFF</li>
						<li class="list-group-item">MUHAMMAD RIDZWAN BIN LOOD</li>
					</ol>
				  </td>
				</tr>
				<tr>
				  <th scope="row" class="align-top bg-primary text-white">PERMOHONAN</th>
				  <td class="">
					<ol class="list-group list-group-numbered">
						<li class="list-group-item">PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN ALAM SEKITAR DAN AIR TAHUN 2020 HINGGA 2023</li>
						<li class="list-group-item">PERKHIDMATAN AUDIT PENGAWASAN ISO/IEC 27001:2013 INFORMATION SECURITY MANAGEMENT SYSTEM (ISMS) KEMENTERIAN SUMBER ASLI, ALAM SEKITAR DAN PERUBAHAN IKLIM (NRECC)</li>
					</ol>
				  </td>
				</tr>
			  </tbody>
			</table>
		</div>
	</div>
	<div class="modal fade" id="confirmationModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-danger">
					<h5 class="modal-title text-white text-truncate">MESYUARAT JKSH BIL. 1/2023</h5>
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
