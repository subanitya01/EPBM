<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="EPBM.auth.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<div class="mb-3">
		<h1 class="h3 d-inline align-middle">Profil Pengguna</h1>
	</div>
	<div class="row">
		<div class="col-md-5 mb-3">
			<div class="card h-100">
				<div class="card-body text-center">
					<img src="/assets/img/avatars/avatar.jpg" alt="Christina Mason" class="img-fluid rounded-circle mb-2" width="128" height="128" />
					<h5 class="card-title mb-0">Charles Hall</h5>
					<div class="text-muted mb-2">Pengurus Projek</div>

					<div>
						<a class="btn btn-outline-primary btn-sm mb-1" href="tel:+6033456789"><span data-feather="phone"></span> 03-3456789</a>
						<a class="btn btn-outline-primary btn-sm mb-1" href="mailto:charles@nrecc.gov.my"><span data-feather="mail"></span> charles@nrecc.gov.my</a>
					</div>
				</div>
			</div>
		</div>
		<div class="col-md-7 mb-3">
			<div class="card h-100">
				<div class="card-header">
					<div class="float-end">
						<a class="btn btn-info btn-sm" href="https://profile.nrecc.gov.my" target="_blank">Kemaskini</a>
					</div>
					<h5 class="card-title mb-0">Maklumat Akaun</h5>
				</div>
				<div class="card-body py-0">
					<table class="table">
						<tbody>
						<tr>
							<th scope="row">Kementerian / Agensi</th>
							<td class="text-secondary">Kementerian Sumber Asli, Alam Sekitar dan Perubahan Iklim</td>
						</tr>
						<tr>
							<th scope="row">Bahagian / Jabatan</th>
							<td class="text-secondary">Bahagian Pengurusan Maklumat</td>
						</tr>
						<tr>
							<th scope="row">Pejabat / Seksyen</th>
							<td class="text-secondary">-</td>
						</tr>
						</tbody>
					</table>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
