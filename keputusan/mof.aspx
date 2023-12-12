<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mof.aspx.cs" Inherits="EPBM.keputusan.mof" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3 d-flex">
		<span class="text-truncate w-100">KEPUTUSAN MOF BAGI <strong><asp:Literal ID="TajukPerolehan" runat="server"></asp:Literal></strong></span>
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
					  <th scope="row" class="align-middle bg-secondary text-white">SYARIKAT DIPERAKU</th>
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
						<label class="control-label" id="lblSyarikat" >SYARIKAT BERJAYA <span class="text-danger">*</span></label>
						<asp:TextBox ID="txtSyarikat" runat="server" CssClass="form-control" placeholder="SYARIKAT BERJAYA" required="required" autocomplete="off" />
						<div class="position-absolute invisible z-3" id="autocompleteSyarikat"></div>
						<span id="syarikatBerjayaLimit" class="text-danger d-none">Syarikat ini telah dilantik melebihi 2 kali dalam 2 tahun</span>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Sila Isi Syarikat Berjaya" ControlToValidate="txtSyarikat" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
	
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label" >TEMPOH <span class="text-danger">*</span></label>
						<div class="input-group">
							<asp:TextBox ID="txtTempoh" runat="server" CssClass="form-control" TextMode="Number" placeholder="TEMPOH" step="1" min="1" required="required" />
							<span class="input-group-text">BULAN</span>
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sila Isi Tempoh" ControlToValidate="txtTempoh" ForeColor="Red"></asp:RequiredFieldValidator>
						<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtTempoh" ErrorMessage="Tempoh hanya dalam digit sahaja" ForeColor="Red" SetFocusOnError="true" />
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label" >NILAI TAWARAN <span class="text-danger">*</span></label>
						<div class="input-group">
							<span class="input-group-text">RM</span>
							<asp:TextBox ID="txtNilaiTawaran" runat="server" CssClass="form-control" TextMode="Number" placeholder="NILAI TAWARAN" required="required" />
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Sila Isi Tempoh" ControlToValidate="txtNilaiTawaran" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
				<div class="col-12">
					<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClick="Save">HANTAR</asp:LinkButton>
				</div>
			</div>
		</div>
	</div>
	<script src="<%= ResolveUrl("~/assets/js/autocomplete.js") %>"></script>
	<script>
        var company_list = <asp:Literal ID="companyList" runat="server" />;
        set_autocomplete('<%=txtSyarikat.ClientID %>', 'autocompleteSyarikat', Object.keys(company_list), 1);
        var inputSyarikat = document.getElementById('<%=txtSyarikat.ClientID %>');
        var errorSyarikat = document.getElementById('syarikatBerjayaLimit');
        inputSyarikat.addEventListener('change', function (event) {
            if (event.target.value in company_list && company_list[event.target.value] != "") {
                errorSyarikat.classList.remove("d-none");
            }
            else errorSyarikat.classList.add("d-none");
		})
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
