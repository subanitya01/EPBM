﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="EPBM.mesyuarat.edit_keputusan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
		<link href="<%= ResolveUrl("~/assets/libs/vanillaSelectBox/vanillaSelectBox.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3 d-flex">
		<span class="text-truncate w-100">KEPUTUSAN <strong><asp:Literal ID="TajukMesyuarat" runat="server"></asp:Literal></strong></span>
		<span class="btn-group btn-group-sm float-end" role="group">
			<asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:history.back()" runat="server" CssClass="btn btn-secondary text-nowrap" ><i class="align-middle" data-feather="corner-up-left"></i> Kembali</asp:HyperLink>
		</span>
	</h1>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">MAKLUMAT PEROLEHAN</h5>
		</div>
		<div class="card-body preview collapse show" id="showDetails">
			<table class="table table-bordered table-hover table-sm text-sm mb-0">
			  <tbody>
				<tr">
					<td scope="row" class="align-middle bg-secondary text-white w-25">TAJUK</td>
					<td class=""><asp:Literal ID="LtlTajuk" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">JENIS PERTIMBANGAN</td>
				  <td class=""><asp:Literal ID="LtlJenisPertimbangan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">KAEDAH PEROLEHAN</td>
				  <td class=""><asp:Literal ID="LtlKaedahPerolehan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">JENIS PEROLEHAN/KONTRAK</td>
				  <td class=""><asp:Literal ID="LtlJenisPerolehan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">JABATAN</td>
				  <td class=""><asp:Literal ID="LtlJabatan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">HARGA INDIKATIF / NILAI KONTRAK</td>
				  <td class="">RM <asp:Literal ID="LtlHarga" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">SUMBER PERUNTUKAN</td>
				  <td class=""><asp:Literal ID="LtlSumberPeruntukan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">TARIKH SAHLAKU TENDER/KONTRAK</td>
				  <td class=""><asp:Literal ID="LtlTarikhSahlaku" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">TARIKH TERIMA</td>
				  <td class=""><asp:Literal ID="LtlTarikhTerima" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <td scope="row" class="align-middle bg-secondary text-white w-25">LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN</td>
				  <td class=""><asp:Literal ID="LtlLulusPelan" runat="server"></asp:Literal></td>
				</tr>
			  </tbody>
			</table>
			<div class="show-full text-center">
				<button type="button" class="btn btn-link w-100" title="Papar Penuh" data-bs-toggle="collapse" data-bs-target="#showDetails">
					<span class="btn-before"><i class="mt-n1" data-feather="chevrons-down"></i ></span>
					<span class="btn-after"><i class="mt-n1" data-feather="chevrons-up"></i ></span>
				</button>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-header bg-info pb-0">
			<h5 class="card-title text-white">PBM MUKTAMAD: <asp:Literal ID="LtlPBMMuktamad" runat="server"></asp:Literal></h5>
		</div>
		<div class="card-body">
				<div class="row">
					<div class="col-12 mb-2">
						<label class="control-label">KEPUTUSAN <span class="text-danger">*</span></label>
						<div class="mt-1">
							<asp:RadioButtonList ID="RadioStatus" RepeatDirection="Horizontal" runat="server" CssClass="table table-borderless table-sm mb-0" />
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Sila Pilih Status" ControlToValidate="RadioStatus" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Sila Pilih Status" ControlToValidate="RadioStatus" ValidationGroup="fail" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
					</div>
				</div>
				<asp:Panel ID="PanelSuccess1" runat="server" CssClass="row success" Visible="false">
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label" id="lblSyarikat" >SYARIKAT BERJAYA <span class="text-danger">*</span></label>
						<asp:TextBox ID="txtSyarikat" runat="server" CssClass="form-control" placeholder="SYARIKAT BERJAYA" required="required" autocomplete="off" />
						<div class="position-absolute invisible z-3" id="autocompleteSyarikat"></div>
						<span id="syarikatBerjayaLimit" class="text-danger d-none">Syarikat ini telah dilantik melebihi 2 kali dalam 2 tahun</span>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Sila Isi Syarikat Berjaya" ControlToValidate="txtSyarikat" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
	
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label" >TEMPOH <span class="text-danger">*</span></label>
						<div class="input-group">
							<asp:TextBox ID="txtTempoh" runat="server" CssClass="form-control" TextMode="Number" placeholder="TEMPOH" step="1" min="1" required="required" />
							<span class="input-group-text">BULAN</span>
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sila Isi Tempoh" ControlToValidate="txtTempoh" ValidationGroup="success" ForeColor="Red"></asp:RequiredFieldValidator>
						<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtTempoh" ErrorMessage="Tempoh hanya dalam digit sahaja" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true" />
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label" >NILAI TAWARAN <span class="text-danger">*</span></label>
						<div class="input-group">
							<span class="input-group-text">RM</span>
							<asp:TextBox ID="txtNilaiTawaran" runat="server" CssClass="form-control" TextMode="Number" placeholder="NILAI TAWARAN" required="required" />
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Sila Isi Tempoh" ControlToValidate="txtNilaiTawaran" ValidationGroup="success" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">LAMPIRAN</label>
						<div class="d-flex position-relative">
							<asp:Label runat="server" id="attachmentLabel" AssociatedControlId="fileAttachment" class="w-100 preview-file position-absolute border bg-white text-truncate d-flex align-items-stretch" title="168187124571_Screenshot 2023-04-04 110107.jpg">
								<span class="py-1 px-3 bg-body-tertiary border">Choose File</span> 
								<span class="py-1 px-2"><asp:Literal ID="LiteralFileName" runat="server"></asp:Literal></span> 
							</asp:Label>
							<asp:FileUpload ID="fileAttachment" runat="server" CssClass="form-control" placeholder="LAMPIRAN" />
							<button id="removeAttachment" class="m-1 bg-white  position-absolute border-0 top-0 end-0 z-10" title="Remove" type="button"><i class="mt-n1" data-feather="x"></i ></button>
						</div>
						<asp:HiddenField ID="keepAttachment" Value="1" runat="server" />
						<asp:RegularExpressionValidator   
							id="FileUpLoadValidator" runat="server"   
							ErrorMessage="Hanya fail PDF, DOCX, DOC, XLSX, XLS, PPTX, PPT, JPG, PNG dibenarkan."   
							ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.[Pp][Dd][Ff]|.[Dd][Oo][Cc]([Xx]?)|.[Xx][Ll][Ss]([Xx]?)|.[Pp][Pp][Tt]([Xx]?)|.[Jj][Pp][Gg]|.[Pp][Nn][Gg])$"   
							ControlToValidate="fileAttachment"
							ValidationGroup="success"
							ForeColor="Red"   
							SetFocusOnError="true">
						</asp:RegularExpressionValidator> 
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

                            var fileAttachment = document.getElementById('<%=fileAttachment.ClientID %>');
                            fileAttachment.addEventListener('change', function (event) {
                                if (fileAttachment.files.length > 0)
                                    document.getElementById('<%=attachmentLabel.ClientID %>').classList.add('d-none');
								else if (document.getElementById('<%=LiteralFileName.ClientID %>').innerText.trim() != "")
									document.getElementById('<%=attachmentLabel.ClientID %>').classList.remove('d-none');
							})
							var removeAttachment = document.getElementById('removeAttachment');
							removeAttachment.addEventListener('click', function (event) {
								document.getElementById('<%=attachmentLabel.ClientID %>').classList.add('d-none');
								document.getElementById('<%=fileAttachment.ClientID %>').value = '';
								document.getElementById('<%=keepAttachment.ClientID %>').value = 0;
							})

                            function changeSyarikat(val) {
                                if (val == 1) {
									document.getElementById('lblSyarikat').innerHTML = 'SYARIKAT BERJAYA <span class="text-danger">*</span>';
									document.getElementById('<%=txtSyarikat.ClientID %>').placeholder = "SYARIKAT BERJAYA";
                                    document.getElementById('<%=RequiredFieldValidator4.ClientID %>').textContent = "Sila Isi Syarikat Berjaya";
								}
								else {
                                    document.getElementById('lblSyarikat').innerHTML = 'SYARIKAT DIPERAKU <span class="text - danger">*</span>';
                                    document.getElementById('<%=txtSyarikat.ClientID %>').placeholder = "SYARIKAT DIPERAKU";
									document.getElementById('<%=RequiredFieldValidator4.ClientID %>').textContent = "Sila Isi Syarikat Diperaku";
								}
                            }
                            const urlParams = new URLSearchParams(window.location.search);
                            changeSyarikat(urlParams.get('muktamad'));
                        </script>
				</asp:Panel>
				<asp:Panel ID="PanelSuccess2" runat="server" CssClass="row success" Visible="false">
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">JENIS PENTADBIRAN KONTRAK <span class="text-danger">*</span></label>
						<asp:ListBox ID="listJenisPentadbiranKontrak" CssClass="form-select" required="required" runat="server" SelectionMode="Multiple"/>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Sila Pilih Jenis Pentadbiran Kontrak" ControlToValidate="listJenisPentadbiranKontrak" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">LAMPIRAN</label>
						<div class="d-flex position-relative">
							<asp:Label runat="server" id="attachmentLabel3" AssociatedControlId="fileAttachment3" class="w-100 preview-file position-absolute border bg-white text-truncate d-flex align-items-stretch">
								<span class="py-1 px-3 bg-body-tertiary border">Choose File</span> 
								<span class="py-1 px-2"><asp:Literal ID="LiteralFileName3" runat="server"></asp:Literal></span> 
							</asp:Label>
							<asp:FileUpload ID="fileAttachment3" runat="server" CssClass="form-control" placeholder="LAMPIRAN" />
							<button id="removeAttachment3" class="m-1 bg-white  position-absolute border-0 top-0 end-0 z-10" title="Remove" type="button"><i class="mt-n1" data-feather="x"></i ></button>
						</div>
						<asp:HiddenField ID="keepAttachment3" Value="1" runat="server" />
						<asp:RegularExpressionValidator   
							id="FileUpLoadValidator3" runat="server"   
							ErrorMessage="Hanya fail PDF, DOCX, DOC, XLSX, XLS, PPTX, PPT, JPG, PNG dibenarkan."   
							ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.[Pp][Dd][Ff]|.[Dd][Oo][Cc]([Xx]?)|.[Xx][Ll][Ss]([Xx]?)|.[Pp][Pp][Tt]([Xx]?)|.[Jj][Pp][Gg]|.[Pp][Nn][Gg])$"   
							ControlToValidate="fileAttachment3"
							ValidationGroup="fail"
							SetFocusOnError="true"
							ForeColor="Red">  
						</asp:RegularExpressionValidator> 
					</div>
					<script src="<%= ResolveUrl("~/assets/libs/vanillaSelectBox/vanillaSelectBox.min.js") %>"></script>
					<script>
						let selectBox = new vanillaSelectBox("#<%=listJenisPentadbiranKontrak.ClientID %>", {
							"maxHeight": 200,
                            placeHolder: "SILA PILIH",
							disableSelectAll: true,
							keepInlineStyles: false,
                            itemsSeparator: ", ",
                            "translations": { "all": "SEMUA", "selectAll": "PILIH SEMUA", "clearAll": "KOSONGKAN", "item": "JENIS", "items": "JENIS" } 
						});
                        var fileAttachment3 = document.getElementById('<%=fileAttachment3.ClientID %>');
                        fileAttachment3.addEventListener('change', function (event) {
                            if (fileAttachment3.files.length > 0)
                                document.getElementById('<%=attachmentLabel3.ClientID %>').classList.add('d-none');
							else if (document.getElementById('<%=LiteralFileName3.ClientID %>').innerText.trim() != "")
								document.getElementById('<%=attachmentLabel3.ClientID %>').classList.remove('d-none');
						})
						var removeAttachment3 = document.getElementById('removeAttachment3');
						removeAttachment3.addEventListener('click', function (event) {
							document.getElementById('<%=attachmentLabel3.ClientID %>').classList.add('d-none');
							document.getElementById('<%=fileAttachment3.ClientID %>').value = '';
							document.getElementById('<%=keepAttachment3.ClientID %>').value = 0;
						})
                    </script>
				</asp:Panel>
				<asp:Panel ID="PanelSuccess3" runat="server" CssClass="row success" Visible="false">
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">LAMPIRAN</label>
						<div class="d-flex position-relative">
							<asp:Label runat="server" id="attachmentLabel4" AssociatedControlId="fileAttachment3" class="w-100 preview-file position-absolute border bg-white text-truncate d-flex align-items-stretch">
								<span class="py-1 px-3 bg-body-tertiary border">Choose File</span> 
								<span class="py-1 px-2"><asp:Literal ID="LiteralFileName4" runat="server"></asp:Literal></span> 
							</asp:Label>
							<asp:FileUpload ID="fileAttachment4" runat="server" CssClass="form-control" placeholder="LAMPIRAN" />
							<button id="removeAttachment4" class="m-1 bg-white  position-absolute border-0 top-0 end-0 z-10" title="Remove" type="button"><i class="mt-n1" data-feather="x"></i ></button>
						</div>
						<asp:HiddenField ID="keepAttachment4" Value="1" runat="server" />
						<asp:RegularExpressionValidator   
							id="FileUpLoadValidator4" runat="server"   
							ErrorMessage="Hanya fail PDF, DOCX, DOC, XLSX, XLS, PPTX, PPT, JPG, PNG dibenarkan."   
							ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.[Pp][Dd][Ff]|.[Dd][Oo][Cc]([Xx]?)|.[Xx][Ll][Ss]([Xx]?)|.[Pp][Pp][Tt]([Xx]?)|.[Jj][Pp][Gg]|.[Pp][Nn][Gg])$"   
							ControlToValidate="fileAttachment4"
							ValidationGroup="fail"
							SetFocusOnError="true"
							ForeColor="Red">  
						</asp:RegularExpressionValidator> 
					</div>
					<div class="col-12 success mb-2">
						<div>
							<label class="control-label">CATATAN <span class="text-danger">*</span></label>
							<asp:TextBox ID="txtAlasan2" runat="server" CssClass="form-control" placeholder="CATATAN" TextMode="MultiLine" Rows="4" />
							<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Sila Isi Catatan" ControlToValidate="txtAlasan2" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
						</div>
					</div>
					<script>
                        var fileAttachment4 = document.getElementById('<%=fileAttachment4.ClientID %>');
                        fileAttachment4.addEventListener('change', function (event) {
                            if (fileAttachment4.files.length > 0)
                                document.getElementById('<%=attachmentLabel4.ClientID %>').classList.add('d-none');
							else if (document.getElementById('<%=LiteralFileName4.ClientID %>').innerText.trim() != "")
								document.getElementById('<%=attachmentLabel4.ClientID %>').classList.remove('d-none');
						})
						var removeAttachment4 = document.getElementById('removeAttachment4');
						removeAttachment4.addEventListener('click', function (event) {
							document.getElementById('<%=attachmentLabel4.ClientID %>').classList.add('d-none');
							document.getElementById('<%=fileAttachment4.ClientID %>').value = '';
							document.getElementById('<%=keepAttachment4.ClientID %>').value = 0;
						})
                    </script>
				</asp:Panel>
				<asp:Panel ID="PanelFail" runat="server" CssClass="row fail d-none">
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">LAMPIRAN</label>
						<div class="d-flex position-relative">
							<asp:Label runat="server" id="attachmentLabel2" AssociatedControlId="fileAttachment2" class="w-100 preview-file position-absolute border bg-white text-truncate d-flex align-items-stretch">
								<span class="py-1 px-3 bg-body-tertiary border">Choose File</span> 
								<span class="py-1 px-2"><asp:Literal ID="LiteralFileName2" runat="server"></asp:Literal></span> 
							</asp:Label>
							<asp:FileUpload ID="fileAttachment2" runat="server" CssClass="form-control" placeholder="LAMPIRAN" />
							<button id="removeAttachment2" class="m-1 bg-white  position-absolute border-0 top-0 end-0 z-10" title="Remove" type="button"><i class="mt-n1" data-feather="x"></i ></button>
						</div>
						<asp:HiddenField ID="keepAttachment2" Value="1" runat="server" />
						<asp:RegularExpressionValidator   
							id="FileUpLoadValidator2" runat="server"   
							ErrorMessage="Hanya fail PDF, DOCX, DOC, XLSX, XLS, PPTX, PPT, JPG, PNG dibenarkan."   
							ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.[Pp][Dd][Ff]|.[Dd][Oo][Cc]([Xx]?)|.[Xx][Ll][Ss]([Xx]?)|.[Pp][Pp][Tt]([Xx]?)|.[Jj][Pp][Gg]|.[Pp][Nn][Gg])$"   
							ControlToValidate="fileAttachment2"
							ValidationGroup="fail"
							SetFocusOnError="true"
							ForeColor="Red">  
						</asp:RegularExpressionValidator> 
					</div>
					<div class="col-12 fail mb-2">
						<div>
							<label class="control-label">CATATAN <span class="text-danger">*</span></label>
							<asp:TextBox ID="txtAlasan" runat="server" CssClass="form-control" placeholder="CATATAN" TextMode="MultiLine" Rows="4" />
							<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Sila Isi Catatan" ControlToValidate="txtAlasan" ValidationGroup="fail" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
						</div>
					</div>
				</asp:Panel>
				<div class="col-12">
					<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary success" ValidationGroup="success" runat="server" OnClick="SaveSuccess">HANTAR</asp:LinkButton>
					<asp:LinkButton ID="btnSubmit2" CssClass="btn btn-primary fail d-none" runat="server" ValidationGroup="fail" OnClick="SaveFail">HANTAR</asp:LinkButton>
				</div>
		</div>
	</div>
	
	<script>
		var statuses = document.querySelectorAll('#<%=RadioStatus.ClientID %> input[type="radio"]');

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
						var notkivfields = field.querySelectorAll(".notkiv");

                        if (status.value == 5) {
                            notkivfields.forEach(notkivfield => {
                                notkivfield.classList.add("d-none");
							})
						}
                        else
                            notkivfields.forEach(notkivfield => {
                                notkivfield.classList.remove("d-none");
                            })
					});
				}
			});
            if (status.checked)
            status.dispatchEvent(new Event('change'));
		});

        var fileAttachment2 = document.getElementById('<%=fileAttachment2.ClientID %>');
        fileAttachment2.addEventListener('change', function (event) {
            if (fileAttachment2.files.length > 0)
                document.getElementById('<%=attachmentLabel2.ClientID %>').classList.add('d-none');
            else if (document.getElementById('<%=LiteralFileName2.ClientID %>').innerText.trim() != "")
                document.getElementById('<%=attachmentLabel2.ClientID %>').classList.remove('d-none');
        })
		var removeAttachment2 = document.getElementById('removeAttachment2');
		removeAttachment2.addEventListener('click', function (event) {
			document.getElementById('<%=attachmentLabel2.ClientID %>').classList.add('d-none');
			document.getElementById('<%=fileAttachment2.ClientID %>').value = '';
			document.getElementById('<%=keepAttachment2.ClientID %>').value = 0;
		})
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
