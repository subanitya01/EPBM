<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="EPBM.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    
<h1 class="h3 mb-3">HALAMAN <strong>UTAMA</strong></h1>
	
	<asp:Panel ID="PanelNotify" runat="server" Visible="false">
		<div class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
			<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
			<div class="alert-icon me-3">
				<i class="mt-n1" data-feather="edit"></i>
			</div>
			<div class="alert-message"><asp:Literal ID="NotifyMsg" runat="server" /></div>
		</div>
	</asp:Panel>
	<div class="d-flex">
		<div class="w-100">
			<div class="row">
				<asp:Panel ID="Panel2Minggu" runat="server" CssClass="col-sm-6 col-xl-4" Visible="false">
					<a href="/segera.aspx">
						<div class="card">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title">TINDAKAN SEGERA</h5>
									</div>

									<div class="col-auto">
										<div class="stat bg-danger">
											<i class="align-middle text-white" data-feather="clock"></i>
										</div>
									</div>
								</div>
								<h3 class="mt-1 mb-3"><asp:Literal ID="Sah2Minggu" runat="server" >0</asp:Literal> Perolehan</h3>
								<div class="mb-0">
									Tempoh sahlaku kurang 2 minggu
								</div>
							</div>
						</div>
					</a>
				</asp:Panel>
				<asp:Panel ID="PanelNextMeeting" runat="server" CssClass="col-sm-6 col-xl-4" Visible="false">
					<asp:HyperLink ID="NextMeetingLink" runat="server" >
						<div class="card">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title">UNTUK PERHATIAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat bg-info">
											<i class="align-middle text-white" data-feather="calendar"></i>
										</div>
									</div>
								</div>
								<h3 class="mt-1 mb-3"><asp:Literal ID="NextMeeting" runat="server" /></h3>
								<div class="mb-0">
									Mesyuarat seterusnya
								</div>
							</div>
						</div>
					</asp:HyperLink>
				</asp:Panel>
				<asp:Panel ID="PanelPertimbangan" runat="server" CssClass="col-sm-6 col-xl-4" Visible="false">
					<a href="/permohonan/senarai.aspx">
						<div class="card">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title">UNTUK PERTIMBANGAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat">
											<i class="align-middle" data-feather="check-square"></i>
										</div>
									</div>
								</div>
								<h3 class="mt-1 mb-3"><asp:Literal ID="BelumKeMesyuarat" runat="server" >0</asp:Literal> Perolehan</h3>
								<div class="mb-0">
									Belum dibawa ke mesyuarat
								</div>
							</div>
						</div>
					</a>
				</asp:Panel>
				<asp:Panel ID="PanelDiputuskan" runat="server" CssClass="col-sm-6 col-xl-4" Visible="true">
					<a href="/keputusan/senarai.aspx">
						<div class="card">
							<div class="card-body">
								<div class="row">
									<div class="col mt-0">
										<h5 class="card-title">MAKLUMAN</h5>
									</div>

									<div class="col-auto">
										<div class="stat">
											<i class="align-middle" data-feather="inbox"></i>
										</div>
									</div>
								</div>
								<h3 class="mt-1 mb-3"><asp:Literal ID="JumlahKeputusan" runat="server" >0</asp:Literal></h3>
								<div class="mb-0">
									Perolehan telah diputuskan
								</div>
							</div>
						</div>
					</a>
				</asp:Panel>
					<asp:Panel ID="PanelPenyemak1" runat="server" CssClass="col-sm-6 col-xl-4" Visible="false">
						<a href="/permohonan/senarai-pengesahan.aspx">
							<div class="card">
								<div class="card-body">
									<div class="row">
										<div class="col mt-0">
											<h5 class="card-title">UNTUK TINDAKAN</h5>
										</div>

										<div class="col-auto">
											<div class="stat bg-warning">
												<i class="align-middle text-white" data-feather="file-text"></i>
											</div>
										</div>
									</div>
									<h3 class="mt-1 mb-3"><asp:Literal ID="PengesahanPerolehan" runat="server" >-</asp:Literal></h3>
									<div class="mb-0">
										Perolehan untuk disahkan
									</div>
								</div>
							</div>
						</a>
					</asp:Panel>
					<asp:Panel ID="PanelPenyemak2" runat="server" CssClass="col-sm-6 col-xl-4" Visible="false">
						<a href="/mesyuarat/pengesahan.aspx">
							<div class="card">
								<div class="card-body">
									<div class="row">
										<div class="col mt-0">
											<h5 class="card-title">UNTUK TINDAKAN</h5>
										</div>

										<div class="col-auto">
											<div class="stat bg-warning">
												<i class="align-middle text-white" data-feather="check-square"></i>
											</div>
										</div>
									</div>
									<h3 class="mt-1 mb-3"><asp:Literal ID="PerakuanMesyuarat" runat="server" >-</asp:Literal></h3>
									<div class="mb-0">
										Mesyuarat untuk disahkan
									</div>
								</div>
							</div>
						</a>
					</asp:Panel>
					<asp:Panel ID="PanelNextMeeting2" runat="server" CssClass="col-sm-6 col-xl-4" Visible="false">
						<asp:HyperLink ID="NextMeetingLink2" runat="server" >
							<div class="card">
								<div class="card-body">
									<div class="row">
										<div class="col mt-0">
											<h5 class="card-title">UNTUK PERHATIAN</h5>
										</div>

										<div class="col-auto">
											<div class="stat">
												<i class="align-middle" data-feather="calendar"></i>
											</div>
										</div>
									</div>
									<h3 class="mt-1 mb-3"><asp:Literal ID="NextMeeting2" runat="server" /></h3>
									<div class="mb-0">
										Mesyuarat seterusnya
									</div>
								</div>
							</div>
						</asp:HyperLink>
					</asp:Panel>
				</div>
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
