<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="daftar.aspx.cs" Inherits="EPBM.mesyuarat.daftar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">DAFTAR <strong>MESYUARAT</strong></h1>
	<div class="card">
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server" defaultbutton="btnSubmit">
				<div class="row">
					<div class="col-12 col-lg-6 mb-3">
						<label class="control-label">JENIS <span class="text-danger">*</span></label>
						<asp:DropDownList ID="ddlJenis" runat="server" CssClass="form-control form-select" required="required"></asp:DropDownList>
					</div>
					<div class="col-12 col-lg-6 mb-3">
						<label class="control-label">BILANGAN <span class="text-danger">*</span></label>
						<div class="input-group">
							<asp:TextBox ID="txtBil" type="number" runat="server" CssClass="form-control" placeholder="BIL" min="1" required="required"></asp:TextBox>
							<span class="input-group-text">/</span>
							<asp:TextBox ID="txtTahun" type="number" runat="server" CssClass="form-control" placeholder="TAHUN" min="1900" max="3000" required="required"></asp:TextBox>
						</div>
					</div>
					<div class="col-12 col-lg-6 mb-3">
						<label class="control-label">TARIKH <span class="text-danger">*</span></label>
						<asp:TextBox ID="txtTarikh" type="date" runat="server" CssClass="form-control" placeholder="TARIKH" required="required"></asp:TextBox>
					</div>
					<div class="col-12">
						<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClick="Save">SIMPAN</asp:LinkButton>
					</div>

				</div>
			</asp:Panel>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
