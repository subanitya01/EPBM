<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="EPBM.permohonan.edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">EDIT <strong>PERMOHONAN</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
				<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
				<div class="alert-icon me-3">
					<i class="mt-n1" data-feather="bell"></i>
				</div>
				<div class="alert-message">
					Terdapat kesilapan pada tarikh terima dan kaedah perolehan!<br>
					Sila semak dengan teliti<br>
					blablaablaaa
				</div>
			</div>
			<div class="row">
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">TAJUK <span class="text-danger">*</span></label>
						<input type="text" class="form-control" placeholder="TAJUK" required value="PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025">
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<label class="control-label">JENIS PERTIMBANGAN <span class="text-danger">*</span></label>
					<div class="input-group mb-3 other-input">
						<select class="form-select" aria-label="JENIS PERTIMBANGAN" required>
						  <option >SILA PILIH</option>
						  <option>PELANTIKAN KONTRAKTOR</option>
						  <option>PENTADBIRAN KONTRAK</option>
						  <option>MUKTAMAD HARGA PK 7</option>
						  <option>MUKTAMAD HARGA PK 2.6</option>
						  <option selected>LAIN-LAIN</option>
						</select>
						<input type="text" class="form-control w-50 d-none" placeholder="LAIN-LAIN">
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">KAEDAH PEROLEHAN <span class="text-danger">*</span></label>
						<select class="form-select mb-3" required>
						  <option>SILA PILIH</option>
						  <option>TENDER TERBUKA</option>
						  <option>TENDER TERBUKA PRA-KELAYAKAN</option>
						  <option selected>RUNDINGAN TERUS (PK 2.4)</option>
						  <option>RUNDINGAN TERUS PK 2.6</option>
						  <option>RUNDINGAN TERUS PK 7</option>
						</select>
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<label class="control-label">JENIS PEROLEHAN / KONTRAK <span class="text-danger">*</span></label>
					<div class="input-group mb-3 other-input">
						<select class="form-select" required>
						  <option >SILA PILIH</option>
						  <option>BEKALAN</option>
						  <option>PERKHIDMATAN</option>
						  <option selected>LAIN-LAIN</option>
						</select>
						<input type="text" class="form-control w-50 d-none" placeholder="LAIN-LAIN">
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">JABATAN <span class="text-danger">*</span></label>
						<select class="form-select mb-3" required>
						  <option >SILA PILIH</option>
							<option value="Pegawai Kementerian">Kementerian Sumber Asli, Alam Sekitar dan Perubahan Iklim</option>
							<option value="JUPEM" selected>Jabatan Ukur dan Pemetaan Malaysia</option>
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
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">HARGA INDIKATIF / NILAI KONTRAK <span class="text-danger">*</span></label>
						<input type="number" class="form-control" placeholder="" step=".01" required value="3456789.00">
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<label class="control-label">SUMBER PERUNTUKAN <span class="text-danger">*</span></label>
					<div class="input-group mb-3 other-input">
						<select class="form-select" required>
						  <option>SILA PILIH</option>
						  <option>BELANJA MENGURUS</option>
						  <option>BELANJA PEMBANGUNAN</option>
						  <option>TABUNG AMANAH</option>
						  <option selected>LAIN-LAIN</option>
						</select>
						<input type="text" class="form-control w-50 d-none" placeholder="LAIN-LAIN">
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">PBM MUKTAMAD <span class="text-danger">*</span></label>
						<select class="form-select mb-3" required>
						  <option>SILA PILIH</option>
						  <option>LP NRECC</option>
						  <option selected>MOF</option>
						</select>
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div class="mb-3">
						<label class="control-label">TARIKH SAHLAKU TENDER / KONTRAK <span class="text-danger">*</span></label>
						<input type="date" class="form-control" placeholder="" required value="2023-12-04">
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div class="mb-3">
						<label class="control-label">TARIKH TERIMA <span class="text-danger">*</span></label>
						<input type="date" class="form-control" placeholder="" required value="2023-10-04">
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<label class="form-check mb-3">
						<input class="form-check-input" type="checkbox" value="" required checked>
						<span class="form-check-label">
						  LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN<span class="text-danger">*</span>
						</span>
					</label>
				</div>
				<div class="col-12">
					<div>
						<label class="control-label">CATATAN</label>
						<textarea class="form-control mb-3" rows="4" placeholder=""></textarea>
					</div>
				</div>
				<div class="col-12">
					<a href="/permohonan/papar.aspx" class="btn btn-primary">KEMASKINI</a>
				</div>

			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<script>
	var otherInputs = document.querySelectorAll('.other-input select');
	otherInputs.forEach(otherInput => {
		otherInput.addEventListener('change', function (event) {console.log(otherInput.value);
			if(otherInput.value=="LAIN-LAIN"){
				otherInput.nextElementSibling.required=true;
				otherInput.nextElementSibling.classList.remove('d-none');
			}
			else{
				otherInput.nextElementSibling.required=false;
				otherInput.nextElementSibling.classList.add('d-none');
			}
		});
		otherInput.dispatchEvent(new Event('change', { 'bubbles': true }));
	});
    </script>
</asp:Content>
