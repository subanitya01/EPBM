﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="papar.aspx.cs" Inherits="EPBM.mesyuarat.papar_keputusan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3 d-flex">
		<span class="text-truncate w-100">PAPAR <strong>KEPUTUSAN <asp:Literal ID="Tajuk" runat="server" Visible="false"></asp:Literal></strong></span>
		<span class="btn-group btn-group-sm float-end" role="group">
			<asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:history.back()" runat="server" CssClass="btn btn-secondary text-nowrap" ><i class="align-middle" data-feather="corner-up-left"></i> Kembali</asp:HyperLink>
		</span>
	</h1>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">MAKLUMAT PEROLEHAN</h5>
		</div>
		<div class="card-body">
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
			</div>
			<table class="table table-bordered table-hover">
			  <tbody>
				<tr>
					<th scope="row" class="align-middle bg-secondary text-white w-25">TAJUK</th>
					<td class=""><asp:Literal ID="LtlTajuk" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">JENIS PERTIMBANGAN</th>
				  <td class=""><asp:Literal ID="LtlJenisPertimbangan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">KAEDAH PEROLEHAN</th>
				  <td class=""><asp:Literal ID="LtlKaedahPerolehan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">JENIS PEROLEHAN/KONTRAK</th>
				  <td class=""><asp:Literal ID="LtlJenisPerolehan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">JABATAN</th>
				  <td class=""><asp:Literal ID="LtlJabatan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">HARGA INDIKATIF / NILAI KONTRAK</th>
				  <td class="">RM <asp:Literal ID="LtlHarga" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">SUMBER PERUNTUKAN</th>
				  <td class=""><asp:Literal ID="LtlSumberPeruntukan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">TARIKH SAHLAKU TENDER/KONTRAK</th>
				  <td class=""><asp:Literal ID="LtlTarikhSahlaku" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">TARIKH TERIMA</th>
				  <td class=""><asp:Literal ID="LtlTarikhTerima" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-secondary text-white">LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN</th>
				  <td class=""><asp:Literal ID="LtlLulusPelan" runat="server"></asp:Literal></td>
				</tr>
			  </tbody>
			</table>
		</div>
	</div>
	<div class="card">
		<div class="card-header pb-0">
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
				<asp:HyperLink ID="LinkToEdit" runat="server" CssClass="btn btn-success" ><i class="mt-n1" data-feather="edit-2"></i > Edit</asp:HyperLink>
			</div>
			<h5 class="card-title">KEPUTUSAN</h5>
		</div>
		<div class="card-body table-responsive">
			<table class="table table-bordered table-hover">
				<tbody>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">MESYUARAT</th>
					  <td class=""><asp:Literal ID="LtlMesyuarat" runat="server"></asp:Literal></td>
					</tr>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white w-25">STATUS</th>
					  <td class=""><asp:Literal ID="LtlStatus" runat="server"></asp:Literal></td>
					</tr>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">PBM MUKTAMAD</th>
					  <td class=""><asp:Literal ID="LtlPbmMuktamad" runat="server"></asp:Literal></td>
					</tr>
					<tr class="success2">
					  <th scope="row" class="align-middle bg-secondary text-white">JENIS PENTADBIRAN KONTRAK</th>
					  <td class=""><asp:Literal ID="LtlJenisPentadbiranKontrak" runat="server"></asp:Literal></td>
					</tr>
					<tr class="success">
					  <th scope="row" class="align-middle bg-secondary text-white"><asp:Literal ID="LtlLabelSyarikat" runat="server">SYARIKAT BERJAYA</asp:Literal></th>
					  <td class=""><asp:Literal ID="LtlSyarikat" runat="server"></asp:Literal></td>
					</tr>
					<tr class="success success2">
					  <th scope="row" class="align-middle bg-secondary text-white">TEMPOH</th>
					  <td class=""><asp:Literal ID="LtlTempoh" runat="server"></asp:Literal> BULAN</td>
					</tr>
					<tr class="success">
					  <th scope="row" class="align-middle bg-secondary text-white">NILAI TAWARAN</th>
					  <td class=""><asp:Literal ID="LtlNilaiTawaran" runat="server"></asp:Literal></td>
					</tr>
					<tr class="fail success2">
					  <th scope="row" class="align-middle bg-secondary text-white">CATATAN</th>
					  <td class=""><asp:Literal ID="LtlAlasan" runat="server"></asp:Literal></td>
					</tr>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">LAMPIRAN</th>
					  <td class=""><asp:HyperLink ID="LinkLampiran" runat="server" Target="_blank" ><i class="mt-n1" data-feather="download"></i > </asp:HyperLink></td>
					</tr>
				</tbody>
			</table>
		</div>
	</div>
	<asp:Panel ID="PanelMOF" runat="server" CssClass="card" Visible="false">
		<div class="card-header pb-0">
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
				<asp:HyperLink ID="LinkToEditMOF" runat="server" CssClass="btn btn-success" ><i class="mt-n1" data-feather="edit-2"></i > Edit</asp:HyperLink>
			</div>
			<h5 class="card-title">KEPUTUSAN MOF</h5>
		</div>
		<div class="card-body table-responsive">
			<table class="table table-bordered table-hover">
				<tbody>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">SYARIKAT BERJAYA</th>
					  <td class=""><asp:Literal ID="LtlSyarikat2" runat="server"></asp:Literal></td>
					</tr>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">TEMPOH</th>
					  <td class=""><asp:Literal ID="LtlTempoh2" runat="server"></asp:Literal> BULAN</td>
					</tr>
					<tr>
					  <th scope="row" class="align-middle bg-secondary text-white">NILAI TAWARAN</th>
					  <td class=""><asp:Literal ID="LtlNilaiTawaran2" runat="server"></asp:Literal></td>
					</tr>
				</tbody>
			</table>
		</div>
	</asp:Panel>
	<asp:Panel ID="PanelSST" runat="server" CssClass="card" Visible="false">
		<div class="card-header pb-0">
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
				<asp:HyperLink ID="LinkToEditSST" runat="server" CssClass="btn btn-success" ><i class="mt-n1" data-feather="edit-2"></i > Edit</asp:HyperLink>
			</div>
			<h5 class="card-title">MAKLUMAT SST</h5>
		</div>
		<div class="card-body table-responsive">
			<table class="table table-bordered table-hover">
				<tbody>
					<tr class="success">
					  <th scope="row" class="align-middle bg-secondary text-white">TARIKH SURAT SETUJU TERIMA</th>
					  <td class=""><asp:Literal ID="LtlTarikhSST" runat="server"></asp:Literal></td>
					</tr>
					<tr class="success">
					  <th scope="row" class="align-middle bg-secondary text-white">RUJUKAN SURAT SETUJU TERIMA</th>
					  <td class=""><asp:Literal ID="LtlRujukanSST" runat="server"></asp:Literal></td>
					</tr>
				</tbody>
			</table>
		</div>
	</asp:Panel>
	<script>
		var statusKeputusan = <asp:Literal ID="LtlIdStatus" runat="server"></asp:Literal>;
		var jenisPertimbangan = <asp:Literal ID="LtlIdJenisPertimbangan" runat="server"></asp:Literal>;
        if ((statusKeputusan == 1 || statusKeputusan == 2) && jenisPertimbangan == 2) {
            failFields = document.querySelectorAll('.fail, .success');
            failFields.forEach(function (field, index) {
                field.classList.add("d-none");
            });
            successFields = document.querySelectorAll('.success2');
            successFields.forEach(function (field, index) {
                field.classList.remove("d-none");
            });
		}
		else if (statusKeputusan == 1 && jenisPertimbangan != 99) {
            failFields = document.querySelectorAll('.fail, .success2');
            failFields.forEach(function (field, index) {
                field.classList.add("d-none");
            });
            successFields = document.querySelectorAll('.success');
            successFields.forEach(function (field, index) {
                field.classList.remove("d-none");
            });
		}
		else {
            successFields = document.querySelectorAll('.success, .success2');
            successFields.forEach(function (field, index) {
                field.classList.add("d-none");
            });
            failFields = document.querySelectorAll('.fail');
            failFields.forEach(function (field, index) {
                field.classList.remove("d-none");
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
