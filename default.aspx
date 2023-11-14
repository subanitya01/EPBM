<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="EPBM.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    
<h1 class="h3 mb-3">HALAMAN <strong>UTAMA</strong></h1>

	<div class="d-flex">
		<div class="w-100">
				<asp:Panel ID="PanelUrusetia" runat="server" Visible="false" CssClass="row">
				<div class="col-sm-6 col-xl-4">
					<a href="/permohonan/senarai.aspx?filter=2minggu">
						<div class="card bg-secondary">
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
								<h3 class="mt-1 mb-3 text-white"><asp:Literal ID="Sah2Minggu" runat="server" >0</asp:Literal> Permohonan</h3>
								<div class="mb-0">
									<span class="text-white">Tempoh sahlaku kurang 2 minggu</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-6 col-xl-4">
					<asp:HyperLink ID="NextMeetingLink" runat="server" >
						<div class="card bg-secondary bg-opacity-75">
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
								<h3 class="mt-1 mb-3 text-white"><asp:Literal ID="NextMeeting" runat="server" /></h3>
								<div class="mb-0">
									<span class="text-white">Mesyuarat seterusnya</span>
								</div>
							</div>
						</div>
					</asp:HyperLink>
				</div>
				<div class="col-sm-6 col-xl-4">
					<a href="/permohonan/senarai.aspx">
						<div class="card bg-secondary bg-opacity-50">
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
								<h3 class="mt-1 mb-3 text-white"><asp:Literal ID="BelumKeMesyuarat" runat="server" >0</asp:Literal> Permohonan</h3>
								<div class="mb-0">
									<span class="text-white">Belum dibawa ke mesyuarat</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-6 col-xl-4 d-none">
					<a href="/keputusan/senarai.aspx">
						<div class="card bg-secondary">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title text-white">MAKLUMAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat text-secondary">
											<i class="align-middle" data-feather="inbox"></i>
										</div>
									</div>
								</div>
								<h3 class="mt-1 mb-3 text-white"><asp:Literal ID="Literal1" runat="server" >-</asp:Literal></h3>
								<div class="mb-0">
									<span class="text-white">Permohonan telah diputuskan</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				</asp:Panel>
				<asp:Panel ID="PanelPenyelia" runat="server" Visible="false" CssClass="row">
				<div class="col-sm-6 col-xl-4">
					<a href="/permohonan/senarai-pengesahan.aspx">
						<div class="card bg-secondary">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title text-white">UNTUK TINDAKAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat text-secondary">
											<i class="align-middle" data-feather="file-text"></i>
										</div>
									</div>
								</div>
								<h3 class="mt-1 mb-3 text-white"><asp:Literal ID="PengesahanPerolehan" runat="server" >-</asp:Literal></h3>
								<div class="mb-0">
									<span class="text-white">Perolehan untuk disahkan</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				<div class="col-sm-6 col-xl-4">
					<a href="/mesyuarat/pengesahan.aspx">
						<div class="card bg-secondary bg-opacity-75">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title text-white">UNTUK TINDAKAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat text-secondary">
											<i class="align-middle" data-feather="check-square"></i>
										</div>
									</div>
								</div>
								<h3 class="mt-1 mb-3 text-white"><asp:Literal ID="PerakuanMesyuarat" runat="server" >-</asp:Literal></h3>
								<div class="mb-0">
									<span class="text-white">Mesyuarat untuk diperakukan</span>
								</div>
							</div>
						</div>
					</a>
				</div>
				</asp:Panel>
			<!--div class="row">
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
			</div-->
		</div>
	</div>
</asp:Content>
