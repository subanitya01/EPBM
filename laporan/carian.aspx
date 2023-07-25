<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="carian.aspx.cs" Inherits="EPBM.laporan.carian" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
	<link href="~/assets/libs/vanillajs-datepicker/css/datepicker-bs5.min.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">CARIAN <strong>LAPORAN</strong></h1>
	<div id="advancedSearch" class="card mt-3">
		<div class="row card-body">
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">JENIS PERTIMBANGAN</label>
					<select class="form-select" aria-label="JENIS PERTIMBANGAN" >
					  <option >SILA PILIH</option>
					  <option>PELANTIKAN KONTRAKTOR</option>
					  <option>PENTADBIRAN KONTRAK</option>
					  <option>MUKTAMAD HARGA PK 7</option>
					  <option>MUKTAMAD HARGA PK 2.6</option>
					  <option>LAIN-LAIN</option>
					</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
						<label class="control-label">KAEDAH PEROLEHAN</label>
						<select class="form-select">
						  <option>SILA PILIH</option>
						  <option>TENDER TERBUKA</option>
						  <option>TENDER TERBUKA PRA-KELAYAKAN</option>
						  <option>RUNDINGAN TERUS (PK 2.4)</option>
						  <option>RUNDINGAN TERUS PK 2.6</option>
						  <option>RUNDINGAN TERUS PK 7</option>
						</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
						<label class="control-label">JENIS PEROLEHAN / KONTRAK</label>
						<select class="form-select">
						  <option >SILA PILIH</option>
						  <option>BEKALAN</option>
						  <option>PERKHIDMATAN</option>
						  <option>LAIN-LAIN</option>
						</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">JABATAN</label>
					<select class="form-select">
					  <option selected>SILA PILIH</option>
						<option value="Pegawai Kementerian">Kementerian Sumber Asli, Alam Sekitar dan Perubahan Iklim</option>
						<option value="JUPEM">Jabatan Ukur dan Pemetaan Malaysia</option>
						<option value="JMG">Jabatan Mineral dan Geosains Malaysia</option>
						<option value="JPSM">Jabatan Perhutanan Semenanjung Malaysia</option>
						<option value="PERHILITAN">Jabatan Perlindungan Hidupan Liar dan Taman Negara</option>
						<option value="FRIM">Insitut Penyelidikan Perhutanan Malaysia</option>
						<option value="INSTUN">Institut Tanah dan Ukur Negara</option>
						<option value="JKPTG">Jabatan Ketua Pengarah Tanah dan Galian</option>
						<option value="NAHRIM">Institut Penyelidikan Hidraulik Kebangsaan Malaysia</option>
						<option value="JBK">Jabatan Biokeselamatan</option>
						<option value="JPS">Jabatan Pengairan dan Saliran</option>
						<option value="JPP">Jabatan Perkhidmatan Pembetungan</option>
						<option value="JAS">Jabatan Alam Sekitar</option>
						<option value="MET">Jabatan Meteorologi Malaysia</option>

					</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
						<label class="control-label">SUMBER PERUNTUKAN</label>
						<select class="form-select">
						  <option>SILA PILIH</option>
						  <option>BELANJA MENGURUS</option>
						  <option>BELANJA PEMBANGUNAN</option>
						  <option>TABUNG AMANAH</option>
						  <option>LAIN-LAIN</option>
						</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
						<label class="control-label">PBM MUKTAMAD</label>
						<select class="form-select">
						  <option>SILA PILIH</option>
						  <option>LP NRECC</option>
						  <option>MOF</option>
						</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">HARGA INDIKATIF</label>
					<select class="form-select">
					  <option>SILA PILIH</option>
					  <option>Kurang daripada 10 ribu</option>
					  <option>10 ribu ke atas dan bawah 100 ribu</option>
					  <option>100 ribu ke atas dan bawah 500 ribu</option>
					  <option>500 ribu dan ke atas</option>
					</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">TARIKH SAHLAKU</label>
					<div class="input-group" id="dateRangePicker">
						<input type="text" class="form-control" placeholder="Mula">
						<span class="input-group-text">Hingga</span>
						<input type="text" class="form-control" placeholder="Tamat" >
					</div>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">MESYUARAT</label>
						<select class="form-select">
						  <option>SILA PILIH</option>
						  <option>MLP BIL. 1/2023</option>
						  <option>JKSH BIL. 1/2023</option>
						</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">SYARIKAT BERJAYA</label>
					<input type="text" class="form-control" placeholder="SYARIKAT BERJAYA">
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">STATUS PERMOHONAN</label>
					<select class="form-select">
					  <option>SILA PILIH</option>
					  <option>BELUM DISAHKAN</option>
					  <option>TIDAK DISAHKAN</option>
					  <option>DISAHKAN</option>
					  <option>BAWA KE MESYUARAT</option>
					  <option>SELESAI</option>
					</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">STATUS KEPUTUSAN</label>
					<select class="form-select">
					  <option>SILA PILIH</option>
					  <option>SELESAI</option>
					  <option>PERINGKAT MOF</option>
					  <option>IKLAN SEMULA</option>
					  <option>BATAL</option>
					</select>
				</div>
				<div class="col-12 text-end">
					<a href="/Application_Report.xlsx" class="btn btn-primary">JANA EXCEL</a>
					<a href="/Application_Report.pdf" class="btn btn-primary">JANA PDF</a>
				</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<script src="~/assets/libs/vanillajs-datepicker/js/datepicker-full.min.js"></script>
	<script>
	const elem = document.getElementById('dateRangePicker');
	const rangepicker = new DateRangePicker(elem, {
	  // ...options
	});
    </script>
</asp:Content>
