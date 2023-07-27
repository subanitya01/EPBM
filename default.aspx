<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="EPBM.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    
<h1 class="h3 mb-3">HALAMAN <strong>UTAMA</strong></h1>

	<div class="d-flex">
		<div class="w-100">
			<div class="row">
				<div class="col-sm-6 col-xl-4">
					<a href="/permohonan/senarai.aspx">
						<div class="card bg-danger">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title text-white">TINDAKAN SEGERA</h5>
									</div>

									<div class="col-auto">
										<div class="stat bg-white">
											<i class="align-middle text-danger" data-feather="clock"></i>
										</div>
									</div>
								</div>
								<h1 class="mt-1 mb-3 text-white">3 Permohonan</h1>
								<div class="mb-0">
									<span class="text-white">Tempoh sahlaku kurang 2 minggu</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-6 col-xl-4">
					<a href="/mesyuarat/papar.aspx">
						<div class="card bg-warning">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title text-white">UNTUK PERHATIAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat bg-white">
											<i class="align-middle text-warning" data-feather="calendar"></i>
										</div>
									</div>
								</div>
								<h1 class="mt-1 mb-3 text-white">3 Hari Lagi</h1>
								<div class="mb-0">
									<span class="text-white">Mesyuarat seterusnya</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-6 col-xl-4">
					<a href="/permohonan/senarai.aspx">
						<div class="card bg-info">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title text-white">UNTUK PERTIMBANGAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat bg-white">
											<i class="align-middle text-info" data-feather="check-square"></i>
										</div>
									</div>
								</div>
								<h1 class="mt-1 mb-3 text-white">4 Permohonan</h1>
								<div class="mb-0">
									<span class="text-white">Belum dibawa ke mesyuarat</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-6 col-xl-4">
					<a href="/keputusan/senarai.aspx">
						<div class="card">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title">MAKLUMAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat text-primary">
											<i class="align-middle" data-feather="inbox"></i>
										</div>
									</div>
								</div>
								<h1 class="mt-1 mb-3">64/120</h1>
								<div class="mb-0">
									<span class="">Permohonan telah diputuskan</span>
								</div>
							</div>
						</div>
					</a>
				</div>
			</div>
			<div class="row">
				<div class="card">
					<div class="card-header pb-0">
						<h5 class="card-title">SYARIKAT YANG MENDAPAT PROJEK 3 KALI DAN KEATAS DALAM TEMPOH 3 TAHUN TERKINI</h5>
					</div>
					<div class="card-body">
						<table class="table table-bordered table-striped table-hover">
						  <thead>
							<tr>
							  <th scope="col">#</th>
							  <th scope="col">SYARIKAT</th>
							  <th scope="col">JUMLAH KONTRAK</th>
							</tr>
						  </thead>
						  <tbody>
							<tr>
							  <th scope="row">1</th>
							  <td class=""><a href="/keputusan/senarai.aspx">BINTARA SOLUTIONS SDN BHD</a></td>
							  <td>3</td>
							</tr>
							<tr>
							  <th scope="row">2</th>
							  <td class=""><a href="/keputusan/senarai.aspx">HIJRAH INOVATIF SDN. BHD.</a></td>
							  <td>5</td>
							</tr>
						  </tbody>
						</table>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
