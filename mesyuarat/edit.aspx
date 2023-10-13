﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="EPBM.mesyuarat.edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<asp:HyperLink ID="LinkToList" NavigateUrl="javascript:history.back()" runat="server" CssClass="btn btn-link ps-0 pt-0" ><i class="align-middle" data-feather="corner-up-left"></i> Kembali</asp:HyperLink>
	<h1 class="h3 mb-3">EDIT <strong>MESYUARAT</strong></h1>
	<asp:Panel ID="Panel1" runat="server">
		<div class="card">
			<div class="card-body">
				<asp:Panel ID="errorMsg" runat="server" Visible="false">
					<div class="alert alert-warning alert-outline alert-dismissible" role="alert">
						<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
						<h4 class="alert-icon text-danger"><i class="align-middle" data-feather="alert-triangle"></i> <strong>Ralat!</strong></h4>
						<div class="alert-message">
							<asp:BulletedList ID="ErrorList" runat="server" />
						</div>
					</div>
				</asp:Panel>
				<div class="row">
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">JENIS <span class="text-danger">*</span></label>
						<asp:DropDownList ID="ddlJenis" runat="server" CssClass="form-control form-select" required="required"></asp:DropDownList>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sila Pilih Jenis Mesyuarat" ControlToValidate="ddlJenis" ValidationGroup="submit" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">BILANGAN <span class="text-danger">*</span></label>
						<div class="input-group">
							<asp:TextBox ID="txtBil" type="number" runat="server" CssClass="form-control" placeholder="BIL" min="1" required="required"></asp:TextBox>
							<span class="input-group-text">/</span>
							<asp:TextBox ID="txtTahun" type="number" runat="server" CssClass="form-control" placeholder="TAHUN" min="1900" max="3000" required="required"></asp:TextBox>
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Sila Isi Bil. Mesyuarat" ControlToValidate="txtBil" ForeColor="Red"></asp:RequiredFieldValidator>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Sila Isi Tahun Bil. Mesyuarat" ControlToValidate="txtTahun" ValidationGroup="submit" ForeColor="Red" CssClass="float-end"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">TARIKH <span class="text-danger">*</span></label>
						<asp:TextBox ID="txtTarikh" type="date" runat="server" CssClass="form-control" placeholder="TARIKH" required="required"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Sila Isi Tarikh Mesyuarat" ControlToValidate="txtTarikh" ValidationGroup="submit" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
				</div>
			</div>
		</div>
		<div class="card">
			<div class="card-header pb-0">
				<h5 class="card-title">AHLI-AHLI MESYUARAT</h5>
			</div>
			<div class="card-body">
						<label class="control-label">PENGERUSI</label>
				<div class="row">
					<div class="col-12 col-lg-6 mb-3">
						<asp:TextBox ID="txtPengerusi" runat="server" CssClass="form-control" placeholder="PENGERUSI"></asp:TextBox>
					</div>
				</div>
				<label class="form-label">AHLI MESYUARAT</label>
				<asp:ScriptManager ID="ScriptManager1" runat="server" />
				
				<asp:UpdatePanel ID="UpdatePanel2" runat="server">
					<ContentTemplate>
							<asp:Repeater ID="Repeater1" runat="server">
								<ItemTemplate>
						<div class="row">
									<div class="col-12 col-lg-6 mb-2">
										<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" RenderMode="inline" UpdateMode="conditional">
											<ContentTemplate>
												<div class="entry input-group">
													<asp:TextBox ID="txtMembers" runat="server" Text='<%# Eval("Nama") %>' AutoPostBack="True" OnTextChanged="updateMember" CssClass="form-control" placeholder="NAMA AHLI" required="required">
													</asp:TextBox>

													<span class="input-group-btn">
														<asp:LinkButton ID="removeBtn" runat="server" CssClass="btn btn-danger" OnClick="removeMember_Click" CommandArgument='<%# Eval("Id") %>'>
															<i class="align-middle" data-feather="minus"></i>
														</asp:LinkButton>
													</span>
												</div>
												<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Sila Isi Nama Ahli Mesyuarat" ControlToValidate="txtMembers" ValidationGroup="submit" ForeColor="Red"></asp:RequiredFieldValidator>
											</ContentTemplate>
											<Triggers>
												<asp:AsyncPostBackTrigger ControlID="txtMembers" EventName="TextChanged" />
												<asp:AsyncPostBackTrigger ControlID="removeBtn" EventName="Click" />
											</Triggers>
										</asp:UpdatePanel>
									</div>
						</div>
								</ItemTemplate>
							</asp:Repeater>
						<div class="row">
							<div class="col-12 col-lg-6 mb-1">
								<div class="entry input-group">
									<asp:TextBox ID="newMember" runat="server" CssClass="form-control" placeholder="NAMA AHLI" required="required" ValidateRequestMode="Enabled">
									</asp:TextBox>

									<span class="input-group-btn">
										<asp:LinkButton ID="addBtn" runat="server" CssClass="btn btn-success" OnClick="addMember_Click" ValidationGroup="newMember">
											<i class="align-middle" data-feather="plus"></i>
										</asp:LinkButton>
									</span>
								</div>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Sila Isi Nama Ahli Mesyuarat" ControlToValidate="newMember" ValidationGroup="newMember" ForeColor="Red"></asp:RequiredFieldValidator>
							</div>
						</div>
					</ContentTemplate>
					<Triggers>
						<asp:AsyncPostBackTrigger ControlID="addBtn" EventName="Click" />
					</Triggers>
				</asp:UpdatePanel>
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
							<asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
								<ItemTemplate>
								  <li class="list-group-item check-input-permohonan bg-white">
									<asp:CheckBox ID="CheckBoxPermohonan" Text='<%# Eval("Tajuk").ToString() %>' runat="server" Checked='<%# !String.IsNullOrEmpty(Eval("IdMesyuarat").ToString()) %>' />
								  </li>
								</ItemTemplate>
							</asp:Repeater>
						</ul>
					</div>

				</div>
			</div>
		</div>
		<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClick="Save" ValidationGroup="submit">SIMPAN</asp:LinkButton>
	</asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<script>
	
	var checkAll = document.querySelector('#checkAll');
    
    checkAll.addEventListener('change', function (event) {
        if (checkAll.checked) {
            getAllCheckBox = document.querySelectorAll('.check-input-permohonan input[type=checkbox]');
			getAllCheckBox.forEach(function (checkbox, index) {
				checkbox.checked = true;
			});
        } else {
            getAllCheckBox = document.querySelectorAll('.check-input-permohonan input[type=checkbox]');
			getAllCheckBox.forEach(function (checkbox, index) {
				checkbox.checked = false;
			});
        }
    });
    </script>
</asp:Content>
