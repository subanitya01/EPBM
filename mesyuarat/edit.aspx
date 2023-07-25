<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="EPBM.mesyuarat.edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">EDIT <strong>MESYUARAT</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="row">
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">JENIS <span class="text-danger">*</span></label>
						<select class="form-select mb-3" aria-label="JENIS" required>
						  <option >SILA PILIH</option>
						  <option>MLP</option>
						  <option selected>JKSH</option>
						</select>
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">BILANGAN <span class="text-danger">*</span></label>
						<div class="input-group">
							<input type="number" class="form-control" placeholder="BIL" value="1" required>
							<span class="input-group-text">/</span>
							<input type="number" class="form-control" placeholder="TAHUN" min="1999" max="2020" value="2023" required>
						</div>
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">TARIKH <span class="text-danger">*</span></label>
						<input type="date" class="form-control mb-3" placeholder="" value="2023-10-04" required>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">AHLI-AHLI MESYUARAT</h5>
		</div>
		<div class="card-body">
			<div class="row">
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">PENGERUSI</label>
						<input type="text" class="form-control mb-3" placeholder="" value="MOHD NORHISHAM BIN MUSA">
					</div>
				</div>
				<div class="col-12">
					<label class="form-label">AHLI MESYUARAT</label>
					<div class="entry input-group mb-3">
					  <input class="form-control" name="members[]" type="text" placeholder="NAMA AHLI" value="ZAENAP BINTI MOHAMAD" />
					  <span class="input-group-btn">
						<button class="btn btn-success btn-remove" type="button">
							<i class="align-middle" data-feather="minus"></i>
						</button>
					  </span>
					</div>
					<div class="entry input-group mb-3">
					  <input class="form-control" name="members[]" type="text" placeholder="NAMA AHLI" value="ZABIR BIN SARIP@SHARIFF" />
					  <span class="input-group-btn">
						<button class="btn btn-success btn-remove" type="button">
							<i class="align-middle" data-feather="minus"></i>
						</button>
					  </span>
					</div>
					<div class="entry input-group mb-3">
					  <input class="form-control" name="members[]" type="text" placeholder="NAMA AHLI" value="MUHAMMAD RIDZWAN BIN LOOD" />
					  <span class="input-group-btn">
						<button class="btn btn-success btn-add" type="button">
							<i class="align-middle" data-feather="plus"></i>
						</button>
					  </span>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">PERMOHONAN</h5>
		</div>
		<div class="card-body">
			<div class="row">
				<div class="col-12">
					<ul class="list-group mb-3">
					  <li class="list-group-item">
						<input class="form-check-input me-1" type="checkbox" value="" aria-label="..." id="checkAll">
						<label class="form-check-label" for="firstRadio">
						<b>SEMUA</b>
						</label>
					  </li>
					  <li class="list-group-item bg-white">
						<input class="form-check-input me-1" type="checkbox" name="papers[]" value="" aria-label="..." checked>
						<label class="form-check-label" for="firstRadio">
						PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025
						</label>
					  </li>
					  <li class="list-group-item bg-white">
						<input class="form-check-input me-1" type="checkbox" name="papers[]" value="" aria-label="..." checked>
						<label class="form-check-label" for="firstRadio">
						PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN ALAM SEKITAR DAN AIR TAHUN 2020 HINGGA 2023
						</label>
					  </li>
					  <li class="list-group-item bg-white">
						<input class="form-check-input me-1" type="checkbox" name="papers[]" value="" aria-label="...">
						<label class="form-check-label" for="firstRadio">
						PERKHIDMATAN AUDIT PENGAWASAN ISO/IEC 27001:2013 INFORMATION SECURITY MANAGEMENT SYSTEM (ISMS) KEMENTERIAN SUMBER ASLI, ALAM SEKITAR DAN PERUBAHAN IKLIM (NRECC)
						</label>
					  </li>
					</ul>
				</div>

			</div>
		</div>
	</div>
	<a href="/mesyuarat/papar.aspx" class="btn btn-primary">SIMPAN</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<script>
	document.addEventListener('click', function (event) {

		console.log(event.target);
		// If the clicked element doesn't have the right selector, bail
		if (event.target.closest('.btn-add')){

			var elem = event.target.closest('.entry');
			var clone = elem.cloneNode(true);
			
			clone.querySelector('input').value = "";
			elem.after(clone);
			event.target.closest('.btn-add').classList.add('btn-remove');
			event.target.closest('.btn-add').classList.remove('btn-add');
			event.target.innerHTML = '<i class="align-middle" data-feather="minus"></i>';
			feather.replace();
			event.preventDefault();
		}
		else if(event.target.closest('.btn-remove')){
			var elem = event.target.closest('.entry');
			elem.parentNode.removeChild(elem);
		}
		
	}, false);
	
	var checkAll = document.querySelector('#checkAll');
    
    checkAll.addEventListener('change', function (event) {
        if (checkAll.checked) {
            getAllCheckBox = document.querySelectorAll('input[type=checkbox][name="papers[]"]');
			getAllCheckBox.forEach(function (checkbox, index) {
				checkbox.checked = true;
			});
        } else {
            getAllCheckBox = document.querySelectorAll('input[type=checkbox][name="papers[]"]');
			getAllCheckBox.forEach(function (checkbox, index) {
				console.log(checkbox);
				checkbox.checked = false;
			});
        }
    });
    </script>
</asp:Content>
