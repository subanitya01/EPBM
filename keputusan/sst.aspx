<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="sst.aspx.cs" Inherits="EPBM.keputusan.sst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3 d-flex">
		<span class="text-truncate w-100">KEMASKINI SST BAGI <strong><asp:Literal ID="TajukPerolehan" runat="server"></asp:Literal></strong></span>
		<span class="btn-group btn-group-sm float-end" role="group">
			<asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:history.back()" runat="server" CssClass="btn btn-secondary text-nowrap" ><i class="align-middle" data-feather="corner-up-left"></i> Kembali</asp:HyperLink>
		</span>
	</h1>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">KEPUTUSAN MESYUARAT <strong><asp:Literal ID="TajukMesyuarat" runat="server"></asp:Literal></strong></h5>
		</div>
		<div class="card-body table-responsive">
			<table class="table table-bordered table-hover table-sm">
				<tbody>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white w-25">STATUS</th>
					  <td class=""><asp:Literal ID="LtlStatus" runat="server"></asp:Literal></td>
					</tr>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">PBM MUKTAMAD</th>
					  <td class=""><asp:Literal ID="LtlPbmMuktamad" runat="server"></asp:Literal></td>
					</tr>
					<tr class="success">
					  <th scope="row" class="align-middle bg-secondary text-white">SYARIKAT BERJAYA</th>
					  <td class=""><asp:Literal ID="LtlSyarikat" runat="server"></asp:Literal></td>
					</tr>
					<tr class="success">
					  <th scope="row" class="align-middle bg-secondary text-white">TEMPOH</th>
					  <td class=""><asp:Literal ID="LtlTempoh" runat="server"></asp:Literal> BULAN</td>
					</tr>
					<tr class="success">
					  <th scope="row" class="align-middle bg-secondary text-white">NILAI TAWARAN</th>
					  <td class="">RM <asp:Literal ID="LtlNilai" runat="server"></asp:Literal></td>
					</tr>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">LAMPIRAN</th>
					  <td class=""><asp:HyperLink ID="LinkLampiran" runat="server" Target="_blank" ><i class="mt-n1" data-feather="download"></i > </asp:HyperLink></td>
					</tr>
				</tbody>
			</table>
		</div>
	</div>
	<div class="card">
		<div class="card-body">
			<div class="row">
				<div class="col-12 col-lg-6 mb-2">
					<label class="control-label">TARIKH SST <span class="text-danger">*</span></label>
					<asp:TextBox ID="txtTarikhSST" type="date" runat="server" CssClass="form-control" placeholder="Tarikh SST" required="required" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Sila Isi Tarikh SST" ControlToValidate="txtTarikhSST" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
				</div>
				<div class="col-12 col-lg-6 mb-2">
					<label class="control-label">RUJUKAN SST <span class="text-danger">*</span></label>
					<asp:TextBox ID="txtRujukanSST" runat="server" CssClass="form-control" placeholder="Rujukan SST" required="required" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Sila Isi Rujukan SST" ControlToValidate="txtRujukanSST" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
				</div>
				<div class="col-12">
					<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClick="Save">HANTAR</asp:LinkButton>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
