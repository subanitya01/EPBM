<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="papar.aspx.cs" Inherits="EPBM.mesyuarat.papar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	
	<h1 class="h3 mb-3 d-flex">
		<span class="text-truncate w-100">MESYUARAT: <strong><asp:Literal runat="server" ID="pageTitle" /></strong></span>
		<span class="btn-group btn-group-sm float-end" role="group">
			<asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:history.back()" runat="server" CssClass="btn btn-secondary text-nowrap" ><i class="align-middle" data-feather="corner-up-left"></i> Kembali</asp:HyperLink>
		</span>
	</h1>
	<div class="card">
		<div class="card-body">
			<asp:Panel ID="actionButtons" runat="server">
				<div class="btn-group btn-group-sm mb-3 float-end" role="group">
					<asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" ><i class="align-middle" data-feather="edit-2"></i> Edit</asp:HyperLink>
					<a href="#" data-bs-toggle="modal" data-bs-target="#deleteModal" class="btn btn-danger"><i class="mt-n1" data-feather="trash"></i> Hapus</a>
					<div class="modal fade" id="deleteModal" tabindex="-1" aria-modal="true" role="dialog">
						<div class="modal-dialog" role="document">
							<div class="modal-content">
								<div class="modal-header bg-danger">
									<h5 class="modal-title text-white text-truncate">MESYUARAT <asp:Literal runat="server" ID="deleteTitle" /></h5>
									<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
								</div>
								<div class="modal-body m-3">
									<p class="mb-0">Anda pasti untuk menghapuskan mesyuarat ini?</p>
								</div>
								<div class="modal-footer">
									<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
									<asp:LinkButton 
										ID="lnkDelete" 
										runat="server" 
										CssClass="btn btn-danger"  
										OnClick="BtnDelete_Click"
									>
										Hapus
									</asp:LinkButton>
								</div>
							</div>
						</div>
					</div>
				</div>
			</asp:Panel>
			<table class="table table-bordered">
			  <tbody>
				<!--tr>
				  <th scope="row" class="align-middle bg-primary text-white w-25">JENIS</th>
				  <td class=""><asp:Literal runat="server" ID="PaparJenis" /></td>
				</!--tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white w-25">BILANGAN</th>
				  <td class=""><asp:Literal runat="server" ID="PaparBil" /></td>
				</tr-->
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white w-25">TARIKH</th>
				  <td class=""><asp:Literal runat="server" ID="PaparTarikh" /></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white w-25">PENGERUSI</th>
				  <td class=""><asp:Literal runat="server" ID="PaparPengerusi" /></td>
				</tr>
				<tr>
				  <th scope="row" class="align-top bg-primary text-white w-25">AHLI MESYUARAT</th>
				  <td class="">
					  <asp:BulletedList id="ListAhli" DisplayMode="Text" CssClass="list-group list-group-numbered" runat="server"></asp:BulletedList>
				  </td>
				</tr>
				<tr>
				  <th scope="row" class="align-top bg-primary text-white w-25">PERMOHONAN</th>
				  <td class="">
					  <asp:BulletedList id="ListPermohonan" DisplayMode="Text" CssClass="list-group list-group-numbered" runat="server"></asp:BulletedList>
				  </td>
				</tr>
			  </tbody>
			</table>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
