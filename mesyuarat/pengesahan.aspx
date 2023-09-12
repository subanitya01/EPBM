<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pengesahan.aspx.cs" Inherits="EPBM.mesyuarat.pengesahan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">PERAKUAN <strong>MESYUARAT</strong></h1>
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
							<a href="#" data-bs-toggle="modal" data-bs-target="#approveModal" class="btn btn-success"><i class="mt-n1" data-feather="check"></i> SAHKAN</a>
							<a href="#" data-bs-toggle="modal" data-bs-target="#rejectModal" class="btn btn-warning"><i class="mt-n1" data-feather="x"></i> KEMBALIKAN</a>
						</div>
					</div>
				</div>
			</asp:Panel>
		</div>
	</div>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">SENARAI PERMOHONAN</h5>
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
						<a href="/keputusan/papar.aspx?id=<%# Eval("Id") %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/keputusan/edit.aspx?id=<%# Eval("Id") %>" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
						
					</ItemTemplate>
                </asp:TemplateField>
            </Columns>
			</asp:GridView>
		</div>
	</div>
	<div class="modal fade" id="approveModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-primary">
					<h5 class="modal-title text-white text-truncate">SAHKAN PERMOHONAN</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="mb-0">Anda pasti untuk mengesahkan permohonan ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-primary" data-bs-dismiss="modal">SAHKAN</button>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="rejectModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-warning">
					<h5 class="modal-title text-white text-truncate">KEMBALIKAN PERMOHONAN</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="">Anda pasti untuk bawa kembalikan permohonan ini untuk pengemaskinian?</p>
					
					<div>
						<textarea class="form-control mb-3" rows="4" placeholder="CATATAN"></textarea>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-warning" data-bs-dismiss="modal">KEMBALIKAN</button>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
