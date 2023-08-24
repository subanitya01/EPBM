﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai.aspx.cs" Inherits="EPBM.mesyuarat.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">SENARAI <strong>MESYUARAT</strong></h1>
	<div class="card">
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server" defaultbutton="btnSubmit">
				<div class="input-group">
					<asp:DropDownList ID="listSearchCol" CssClass="form-select" runat="server" >  
						<asp:ListItem Value="">SEMUA KOLUM</asp:ListItem>  
						<asp:ListItem>JENIS</asp:ListItem>  
						<asp:ListItem>BILANGAN</asp:ListItem>  
						<asp:ListItem>TARIKH</asp:ListItem>  
						<asp:ListItem>PENGERUSI</asp:ListItem>  
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

                <asp:BoundField DataField="JENIS" HeaderText="JENIS" SortExpression="JENIS" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                </asp:BoundField>

                <asp:BoundField DataField="BILANGAN" HeaderText="BILANGAN" SortExpression="BILANGAN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                </asp:BoundField>

				<asp:TemplateField HeaderText="TARIKH" SortExpression="TARIKH" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
					<ItemTemplate>
						<%# Eval("TARIKHMS") %>
					</ItemTemplate>
				</asp:TemplateField>

                <asp:BoundField DataField="PENGERUSI" HeaderText="PENGERUSI" SortExpression="PENGERUSI">
                </asp:BoundField>

				<asp:TemplateField HeaderText="JUMLAH KELULUSAN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
					<ItemTemplate>
						<%# Eval("JumlahKelulusan") %>/<%# Eval("JumlahPermohonan") %>
					</ItemTemplate>
				</asp:TemplateField>

                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
						<a href="/mesyuarat/papar.aspx?id=<%# Eval("Id") %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/mesyuarat/edit.aspx?id=<%# Eval("Id") %>" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
						<a href="/mesyuarat/senarai-keputusan.aspx?id=<%# Eval("Id") %>" class="text-success" title="Keputusan"><i class="align-middle" data-feather="inbox"></i></a>
						<asp:LinkButton 
							ID="lnkDelete" 
							runat="server" 
							CssClass="text-danger"  
							data-bs-toggle="modal" 
							data-bs-target="#deleteModal" 
							title="Hapus"
							CommandArgument='<%# Eval("Id") %>' 
							CausesValidation="false"
							OnClick="BtnDelete_Click"
							OnClientClick='<%# string.Concat("if(!popup(this",",\"MESYUARAT ",Eval("JENIS")," Bil. ",Eval("BILANGAN"),"\"))return false; ") %>'
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
					<p class="mb-0">Anda pasti untuk menghapuskan mesyuarat ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<a href="#" type="button" class="btn btn-danger">Hapus</a>
				</div>
			</div>
		</div>
	</div>
    <script>   
        function popup(lnk, title) {
            document.querySelector("#deleteModal .modal-header h5").innerText = title;
            document.querySelector("#deleteModal .modal-footer a").setAttribute('href', lnk.getAttribute('href'));

        }     
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
