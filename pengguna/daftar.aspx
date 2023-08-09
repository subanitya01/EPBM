﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="daftar.aspx.cs" Inherits="EPBM.pengguna.daftar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">TAMBAH <strong>PENGGUNA</strong></h1>
	<div class="card">
		<div class="card-header pb-0">
		<h5 class="card-title">CARIAN DI SISTEM PROFILE</h5>
		</div>
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server" defaultbutton="btnSearch">
				<div class="input-group">
					<asp:DropDownList ID="listSearchCol" CssClass="form-select" runat="server" >  
						<asp:ListItem Value="">SEMUA KOLUM</asp:ListItem>  
						<asp:ListItem>NAMA</asp:ListItem>  
						<asp:ListItem>NO. K/P</asp:ListItem>  
						<asp:ListItem>E-MEL</asp:ListItem>  
					</asp:DropDownList>
					<asp:TextBox ID="txtSearch" CssClass="form-control w-25" placeholder="Carian..." required="required" runat="server"></asp:TextBox>
					<asp:LinkButton ID="btnSearch" CssClass="btn btn-primary" runat="server" OnClick="BtnSearch_Click"><i class="align-middle" data-feather="search"></i></asp:LinkButton>
				</div>
			</asp:Panel>
		</div>
	</div>
	<asp:Panel ID="Panel2" runat="server" CssClass="card" Visible="false">
		<div class="card-body">
			<div class="row input-group-sm justify-content-between">
				<div class="col-sm-6 col-md-5 mb-3">
				</div>
				<div class="col-sm-3 col-md-2 mb-3 text-end">
					
				</div>
			</div>
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				OnRowDataBound="GridView1_OnRowDataBound"
				AutoGenerateColumns="False" 
				CssClass="table table-bordered table-striped table-hover" 
				OnPageIndexChanging="GridView1_PageIndexChanging" 
				AllowPaging="True" 
				AllowCustomPaging="true"
				AllowSorting="False"
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
    
					<asp:BoundField DataField="Exist" Visible="false" >
					</asp:BoundField>

					<asp:TemplateField ItemStyle-CssClass="text-center">
						<ItemTemplate>
							<asp:Label ID="lblExist" CssClass="text-primary" runat="server" title="Telah Berdaftar" Visible="false"><i class="align-middle" data-feather="check-circle"></i></asp:Label>
							<asp:LinkButton 
								ID="BtnAddUser" 
								runat="server" 
								CssClass="text-secondary"  
								data-bs-toggle="modal" 
								data-bs-target="#addModal" 
								title="Tambah"
								CommandArgument='<%# Eval("NO K/P") %>'
								OnClientClick='<%# string.Concat("if(!popup(this",",",Eval("[NO K/P]"),",\"",Eval("NAMA"),"\"))return false; ") %>'
							>
								<i class="align-middle" data-feather="plus-circle"></i>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</div>
		<div class="modal fade" id="addModal" tabindex="-1" aria-modal="true" role="dialog">
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
						   runat="server">
					  </asp:CheckBoxList>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
						<a href="#" type="button" class="btn btn-primary">Tambah</a>
					</div>
				</div>
			</div>
		</div>
	</asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
        <script>    
			function popup(lnk, id, Name) {    
                document.querySelector("#addModal .modal-header h5").innerText = Name + " (" + id + ")";
                document.querySelector("#addModal .modal-footer a").setAttribute('href', lnk.getAttribute('href'));
                //__doPostBack('ctl00$ContentPlaceHolder3$GridView1$ctl02$lnkDelete','')
                /*    BootstrapDialog.confirm({    
                    title: 'WARNING',    
                    message: 'Do You Want To Delete <b>'+Name+'</b>',    
                    type: BootstrapDialog.TYPE_WARNING, // <-- Default value is BootstrapDialog.TYPE_PRIMARY    
                    closable: true, // <-- Default value is false    
                    draggable: true, // <-- Default value is false    
                    btnCancelLabel: 'Cancel', // <-- Default value is 'Cancel',    
                    btnOKLabel: 'Ok', // <-- Default value is 'OK',    
                    btnOKClass: 'btn-warning', // <-- If you didn't specify it, dialog type will be used,    
                    callback: function (result) {    
                        // result will be true if button was click, while it will be false if users close the dialog directly.    
                        if (result) {    
                             javascript: __doPostBack('grdDemo$ctl02$lnkDelete', '');    
    
                        } else {    
                            BootstrapDialog.closeAll();    
                        }    
                    }    
                }); */   
    
            }    
        </script>  
</asp:Content>
