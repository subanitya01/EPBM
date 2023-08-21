<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai.aspx.cs" Inherits="EPBM.pengguna.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">SENARAI <strong>PENGGUNA</strong></h1>
	<div class="card">
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server" defaultbutton="btnSubmit">
				<div class="input-group">
					<asp:DropDownList ID="listSearchCol" CssClass="form-select" runat="server" >  
						<asp:ListItem Value="">SEMUA KOLUM</asp:ListItem>  
						<asp:ListItem>NAMA</asp:ListItem>  
						<asp:ListItem>NO. K/P</asp:ListItem>  
						<asp:ListItem>E-MEL</asp:ListItem>  
						<asp:ListItem>PENEMPATAN</asp:ListItem>  
						<asp:ListItem>PERANAN</asp:ListItem>  
					</asp:DropDownList>
					<asp:TextBox ID="txtSearch" CssClass="form-control w-25" placeholder="Carian..." runat="server"></asp:TextBox>
					<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClick="Search" CausesValidation="false"><i class="align-middle" data-feather="search"></i></asp:LinkButton>
				</div>
			</asp:Panel>
		</div>
	</div>
	<div class="card">
		<div class="card-body">
			<div class="row input-group-sm justify-content-between">
				<div class="col-sm-6 col-md-5 mb-3">
					<label>SUSUNAN BERDASARKAN: <asp:Label ID="lblSortRecord" runat="server" /></label>
				</div>
				<div class="col-sm-3 col-md-2 mb-3 text-end">
					
				</div>
			</div>
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				OnDataBound="GridView1_DataBound" 
				OnRowDataBound="GridView1_OnRowDataBound" 
				AutoGenerateColumns="False" 
				CssClass="table table-bordered table-striped table-hover" 
				OnPageIndexChanging="GridView1_PageIndexChanging" 
				OnSorting="GridView1_Sorting" 
				AllowPaging="True" 
				AllowSorting="true"
				PageSize="20">  
			<PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="5" FirstPageText="&laquo;" LastPageText="&raquo;" />
			<PagerStyle HorizontalAlign = "Right" CssClass = "bs-pager" />
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="NAMA" HeaderText="NAMA" SortExpression="NAMA">
                </asp:BoundField>

                <asp:BoundField DataField="NO K/P" HeaderText="NO K/P" SortExpression="NO K/P">
                </asp:BoundField>

				<asp:TemplateField HeaderText="E-MEL" SortExpression="E-MEL">
					<ItemTemplate>
						<a href="mailto:<%# Eval("E-MEL") %>"><%# Eval("E-MEL") %></a>
					</ItemTemplate>
				</asp:TemplateField>
    
                <asp:BoundField DataField="PENEMPATAN" HeaderText="PENEMPATAN" SortExpression="PENEMPATAN">
                </asp:BoundField>

                <asp:TemplateField HeaderText="PERANAN" Visible="true">
                    <ItemTemplate>
						<asp:ListView runat="server"  ID="RoleList">
							<LayoutTemplate>
								<ul class="list-group text-sm">
									<li id="itemPlaceholder" runat="server" />
								</ul>
							</LayoutTemplate>
							<ItemTemplate>
								<li class="list-group-item p-1"><%#: Eval("Role") %></li>
							</ItemTemplate>
						</asp:ListView>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemStyle Width="1.5%" Wrap="true" />
                    <ItemTemplate>
                        <asp:Label ID="lblStatusActive" CssClass="badge text-bg-primary" runat="server" Text='Aktif'></asp:Label>
                        <asp:Label ID="lblStatusInactive" CssClass="badge text-bg-danger" runat="server" Text='Tidak Aktif' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
						<asp:LinkButton 
								ID="BtnEditUser" 
								runat="server" 
								CssClass="text-secondary"  
								data-bs-toggle="modal" 
								data-bs-target="#editModal" 
								title="Edit"
								CommandArgument='<%# Eval("Id") %>' 
								OnClick="BtnEditUser_Click"
								OnClientClick='<%# string.Concat("if(!popupEdit(this",",\"",Eval("[NO K/P]"),"\",\"",Eval("NAMA"),"\"))return false; ") %>'
							>
								<i class="align-middle" data-feather="edit-2"></i>
						</asp:LinkButton>
						<asp:LinkButton 
							ID="lnkDelete" 
							runat="server" 
							CssClass="text-danger"  
							data-bs-toggle="modal" 
							data-bs-target="#deleteModal" 
							title="Hapus"
							CommandArgument='<%# Eval("Id") %>' 
							CausesValidation="false"
							OnClick="BtnDeleteUser_Click"
							OnClientClick='<%# string.Concat("if(!popup(this",",",Eval("[NO K/P]"),",\"",Eval("NAMA"),"\"))return false; ") %>'
						>
							<i class="align-middle" data-feather="trash"></i>
						</asp:LinkButton>
					</ItemTemplate>
                </asp:TemplateField>
            </Columns>
			</asp:GridView>
		</div>
	</div>

	<div class="modal fade" id="deleteModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-danger">
					<h5 class="modal-title text-white text-truncate">???</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="mb-0">Anda pasti untuk menghapuskan pengguna ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<a href="#" type="button" class="btn btn-danger">Hapus</a>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="editModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-primary">
					<h5 class="modal-title text-white text-truncate">???</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<h5 class="">Sila Pilih Peranan:</h5>
					<asp:CheckBoxList id="CheckBoxList1" 
						AutoPostBack="false"
						DataValueField="roles"
						CellPadding="5"
						CellSpacing="5"
						RepeatColumns="1"
						RepeatDirection="Vertical"
						RepeatLayout="Flow"
						TextAlign="Right" 
						CssClass="d-block"
						runat="server">
					</asp:CheckBoxList>
					<asp:CustomValidator 
						ID="RequiredRoleValidator" 
						ClientValidationFunction="ValidateCheckBoxList"
						runat="server" 
						CssClass="text-danger"
						ErrorMessage="Sila pilih sekurang-kurangnya satu peranan!">
					</asp:CustomValidator>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<a href="#" type="button" class="btn btn-primary">Simpan</a>
				</div>
			</div>
		</div>
	</div>
    <script>   
        function popup(lnk, id, Name) {
            document.querySelector("#deleteModal .modal-header h5").innerText = Name + " (" + id + ")";
            document.querySelector("#deleteModal .modal-footer a").setAttribute('href', lnk.getAttribute('href'));

        }     
        function popupEdit(lnk, id, Name) {
            document.querySelector("#editModal .modal-header h5").innerText = Name + " (" + id + ")";
			document.querySelector("#editModal .modal-footer a").setAttribute('href', lnk.getAttribute('href'));
            var userRoles = JSON.parse(lnk.getAttribute("data-roles"));
            userRoles.forEach((role) => {
                document.querySelector('#<%=CheckBoxList1.ClientID %> input[type="checkbox"][value="' + role + '"]').checked = true;
			});
            /*for (const checkbox of document.querySelectorAll('.myCheckBox')) {
                //iterating over all matched elements

                checkbox.checked = true //for selection
                checkbox.checked = false //for unselection
            }
            var checkBoxList = document.getElementById("<%=CheckBoxList1.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }*/
		} 

        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=CheckBoxList1.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>  
</asp:Content>