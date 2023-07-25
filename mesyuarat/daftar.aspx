<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="daftar.aspx.cs" Inherits="EPBM.mesyuarat.daftar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">DAFTAR <strong>MESYUARAT</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="row">
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">JENIS <span class="text-danger">*</span></label>
						<select class="form-select mb-3" aria-label="JENIS" required>
						  <option >SILA PILIH</option>
						  <option>MLP</option>
						  <option>JKSH</option>
						</select>
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">BILANGAN <span class="text-danger">*</span></label>
						<div class="input-group">
							<input type="number" class="form-control" placeholder="BIL" value="2" required>
							<span class="input-group-text">/</span>
							<input type="number" class="form-control" placeholder="TAHUN" min="1999" max="2020" value="2023" required>
						</div>
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">TARIKH <span class="text-danger">*</span></label>
						<input type="date" class="form-control mb-3" placeholder="" required>
					</div>
				</div>
				<div class="col-12">
					<a href="/mesyuarat/senarai.aspx" class="btn btn-primary">SIMPAN</a>
				</div>

			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
