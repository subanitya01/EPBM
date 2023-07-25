<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="EPBM.mesyuarat.edit_keputusan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-4 text-truncate">KEPUTUSAN <strong>MESYUARAT JKSH BIL. 1/2023</strong></h1>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">MAKLUMAT PERMOHONAN</h5>
		</div>
		<div class="card-body collapse" id="showDetails">
			<table class="table table-bordered table-hover">
			  <tbody>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TAJUK</th>
				  <td class="">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PERTIMBANGAN</th>
				  <td class="">PENTADBIRAN KONTRAK</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">KAEDAH PEROLEHAN</th>
				  <td class="">RUNDINGAN TERUS (PK 2.4)</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PEROLEHAN/KONTRAK</th>
				  <td class="">PERKHIDMATAN</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JABATAN</th>
				  <td class="">JABATAN UKUR DAN PEMETAAN MALAYSIA</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">HARGA INDIKATIF / NILAI KONTRAK</th>
				  <td class="">RM 3,456,789.00</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">SUMBER PERUNTUKAN</th>
				  <td class="">BELANJA MENGURUS</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH SAHLAKU TENDER/KONTRAK</th>
				  <td class="">04 Disember 2023</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH TERIMA</th>
				  <td class="">04 Oktober 2023</td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN</th>
				  <td class="">YA</td>
				</tr>
			  </tbody>
			</table>
			<div class="show-full text-center">
				<button type="button" class="btn btn-outline-primary rounded-pill" data-bs-toggle="collapse" data-bs-target="#showDetails">
					<span class="btn-before">Papar Penuh <i class="mt-n1" data-feather="chevrons-down"></i ></span>
					<span class="btn-after">Papar Sedikit <i class="mt-n1" data-feather="chevrons-up"></i ></span>
				</button>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-body">
			<div class="row">
				<div class="col-12 mb-3">
					<label class="control-label">STATUS <span class="text-danger">*</span></label>
					<div class="mt-1">
						<div class="form-check form-check-inline">
						  <input value="1" class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="option1" checked>
						  <label class="form-check-label" for="inlineRadio1">SELESAI</label>
						</div>
						<div class="form-check form-check-inline">
						  <input value="2" class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
						  <label class="form-check-label" for="inlineRadio2">PERINGKAT MOF</label>
						</div>
						<div class="form-check form-check-inline">
						  <input value="3" class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
						  <label class="form-check-label" for="inlineRadio2">IKLAN SEMULA</label>
						</div>
						<div class="form-check form-check-inline">
						  <input value="4" class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
						  <label class="form-check-label" for="inlineRadio2">BATAL</label>
						</div>
					</div>
				</div>
				<div class="col-12 col-lg-6 mb-3 success">
					<div>
						<label class="control-label">SYARIKAT BERJAYA <span class="text-danger">*</span></label>
						<input type="text" class="form-control" placeholder="SYARIKAT BERJAYA" value="BINTARA SOLUTIONS SDN BHD">
					</div>
				</div>
				<div class="col-12 col-lg-6 mb-3 success">
					<div>
						<label class="control-label">TEMPOH <span class="text-danger">*</span></label>
						<div class="input-group">
						<input type="number" class="form-control" placeholder="TEMPOH" step="1" min="1" value="2">
						<span class="input-group-text">BULAN</span>
						</div>
					</div>
				</div>
				<div class="col-12 col-lg-6 success">
					<div>
						<label class="control-label">PBM MUKTAMAD <span class="text-danger">*</span></label>
						<select class="form-select mb-3" required>
						  <option>SILA PILIH</option>
						  <option>LP NRECC</option>
						  <option selected>MOF</option>
						</select>
					</div>
				</div>
				<div class="col-12 col-lg-6 success">
					<div>
						<label class="control-label">TARIKH SURAT SETUJU TERIMA</label>
						<input type="date" class="form-control mb-3" placeholder="TARIKH SURAT SETUJU TERIMA" value="2023-10-25">
					</div>
				</div>
				<div class="col-12 col-lg-6 success">
					<div>
						<label class="control-label">RUJUKAN SURAT SETUJU TERIMA</label>
						<input type="text" class="form-control mb-3" placeholder="RUJUKAN SURAT SETUJU TERIMA" value="SK-XXXXXXXX">
					</div>
				</div>
				<div class="col-12 col-lg-6 success fail">
					<label class="control-label">LAMPIRAN</label>
					<div class="d-flex position-relative">
						<label id="attachmentLabel" for="attachment" class="preview-file position-absolute border-top border-start bg-white text-truncate d-flex align-items-stretch" title="168187124571_Screenshot 2023-04-04 110107.jpg">
							<span class="py-1 px-3 bg-body-tertiary border">Choose File</span> 
							<span class="py-1 px-2">168187124571_Screenshot 2023-04-04 110107.jpg</span> 
						</label>
						<input type="file" id="attachment" class="form-control mb-3" placeholder="LAMPIRAN">
						<button id="removeAttachment" class="m-1 bg-white  position-absolute border-0 top-0 end-0 z-10" title="Remove" type="button"><i class="mt-n1" data-feather="x"></i ></button>
					</div>
					<input type="hidden" name="keepAttachment" value="1" />
				</div>
				<div class="col-12 fail d-none">
					<div>
						<label class="control-label">ALASAN</label>
						<textarea class="form-control mb-3" rows="4" placeholder="ALASAN">HARGA DIKEMUKAKAN MELEBIHI NILAI MAKSIMUM DAN TIADA SUMBER BAJET</textarea>
					</div>
				</div>
				<div class="col-12">
					<a href="/keputusan/papar.aspx" class="btn btn-primary">HANTAR</a>
				</div>

			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<script>
	var statuses = document.querySelectorAll('input[name="inlineRadioOptions"]');
    statuses.forEach(status => {
		status.addEventListener('change', function (event) {
            if (status.value == 1 || status.value == 2) {
                failFields = document.querySelectorAll('.fail');
                failFields.forEach(function (field, index) {
                    field.classList.add("d-none");
                });
				successFields = document.querySelectorAll('.success');
				successFields.forEach(function (field, index) {
					field.classList.remove("d-none");
				});
			} else {
				successFields = document.querySelectorAll('.success');
				successFields.forEach(function (field, index) {
					field.classList.add("d-none");
				});
				failFields = document.querySelectorAll('.fail');
				failFields.forEach(function (field, index) {
					field.classList.remove("d-none");
				});
			}
		});
    });
	
	var removeAttachment = document.getElementById('removeAttachment');
	removeAttachment.addEventListener('click', function (event) {
		document.getElementById('attachmentLabel').classList.add('d-none');
		document.getElementById('attachment').value = '';
		document.querySelector('input[name="keepAttachment"]').value = 0;
	})
    </script>
</asp:Content>
