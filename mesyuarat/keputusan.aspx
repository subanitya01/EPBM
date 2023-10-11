<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="keputusan.aspx.cs" Inherits="EPBM.mesyuarat.keputusan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">KEPUTUSAN <b>MESYUARAT</b></h1>
	<asp:Panel ID="PanelNotFound" runat="server">
		<div class="alert alert-info d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
			<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
			<div class="alert-icon me-3">
				<i class="mt-n1" data-feather="info"></i>
			</div>
			<div class="alert-message">Tiada mesyuarat untuk diputuskan buat masa ini.</div>
		</div>
	</asp:Panel>
	<asp:Panel ID="PanelFound" runat="server" Visible="false">
		<asp:Panel ID="PanelComment" runat="server" Visible="false">
			<div class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
				<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
				<div class="alert-icon me-3">
					<i class="mt-n1" data-feather="bell"></i>
				</div>
				<div class="alert-message"><asp:Literal ID="CatatanPengesahan" runat="server" /></div>
			</div>
		</asp:Panel>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">MESYUARAT</h5>
		</div>
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server">
				<div class="input-group row">
					<div class="col-md-6">
					<asp:DropDownList ID="listMesyuarat" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RefreshPage" >  
					</asp:DropDownList>
					</div>
					<div class="col-md-6">
						<div class="btn-group btn-group-sm mb-3 float-end" role="group">
							<asp:LinkButton ID="confirmBtn" CssClass="btn btn-info" data-bs-toggle="modal" data-bs-target="#meetingModal" runat="server">
								<i class="mt-n1" data-feather="send"></i> Hantar Untuk Pengesahan
							</asp:LinkButton>
						</div>
					</div>
				</div>
				
				<div class="modal fade" id="meetingModal" tabindex="-1" aria-modal="true" role="dialog">
					<div class="modal-dialog" role="document">
						<div class="modal-content">
							<div class="modal-header bg-info">
								<h5 class="modal-title text-white text-truncate">MESYUARAT <asp:Literal ID="TajukMesyuaratModal" runat="server"></asp:Literal></h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
							</div>
							<div class="modal-body m-3">
								<p class="">Anda pasti hantar mesyuarat ini untuk kelulusan penyelia?</p>
							</div>
							<div class="modal-footer">
								<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
								<asp:LinkButton ID="sendBtn" runat="server" CssClass="btn btn-info" OnClick="BtnSend_Click">
									Hantar
								</asp:LinkButton>
							</div>
						</div>
					</div>
				</div>
			</asp:Panel>
		</div>
	</div>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">SENARAI PERMOHONAN <asp:Literal ID="TajukPermohonan" runat="server" /></h5>
		</div>
		<div class="card-body table-responsive">
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				AutoGenerateColumns="False" 
				OnRowDataBound="GridView1_OnRowDataBound" 
				CssClass="table table-bordered table-striped table-hover">  

				<Columns>
					<asp:TemplateField HeaderText="#">
						<ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
					</asp:TemplateField>

					<asp:BoundField DataField="TAJUK" HeaderText="TAJUK">
					</asp:BoundField>

					<asp:BoundField DataField="JABATAN" HeaderText="JABATAN" HeaderStyle-CssClass="text-center">
					</asp:BoundField>

					<asp:TemplateField HeaderText="STATUS" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
						<ItemTemplate>
							<asp:Label ID="lblStatus" CssClass="badge" runat="server"><%# Eval("STATUS") %></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField HeaderText="KETERANGAN" HeaderStyle-CssClass="text-center">
						<ItemTemplate>
							<asp:Label ID="LblKeterangan" runat="server"><%# Eval("KETERANGAN").ToString().Replace(Environment.NewLine, "<br />") %></asp:Label>
						
							<asp:ListView runat="server"  ID="DetailsList">
								<LayoutTemplate>
									<ul class="list-group text-sm">
										<li id="itemPlaceholder" runat="server" />
									</ul>
								</LayoutTemplate>
								<ItemTemplate>
									<li class="list-group-item p-1"><b><%#: Eval("Label") %>:</b> <%#: Eval("Text") %></li>
								</ItemTemplate>
							</asp:ListView>
						</ItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField ItemStyle-CssClass="text-center">
						<ItemTemplate>
							<a href="/keputusan/papar.aspx?id=<%# Eval("Id") %>&ReturnURL=<%# System.Web.HttpUtility.UrlEncode("/mesyuarat/pengesahan.aspx?id=" + Eval("IdMesyuarat")) %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
							<a href="/keputusan/edit.aspx?id=<%# Eval("Id") %>&ReturnURL=<%# System.Web.HttpUtility.UrlEncode("/mesyuarat/pengesahan.aspx?id=" + Eval("IdMesyuarat")) %>" class="text-secondary" title="Edit Keputusan"><i class="align-middle" data-feather="edit-2"></i></a>
						
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</div>
	</div>
	</asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>