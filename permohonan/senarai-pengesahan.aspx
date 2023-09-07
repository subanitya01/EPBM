<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="senarai-pengesahan.aspx.cs" Inherits="EPBM.permohonan.pengesahan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


<link href="../assets/css/gridview.css" rel="stylesheet" />

<style>
	img {
padding-top: 10px;
width: 22px;
height: 27px;
}

body {
  overflow-y: auto; /* Hide vertical scrollbar */
  overflow-x: auto; /* Hide horizontal scrollbar */
}

</style>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

<asp:ScriptManager ID="ScriptManager1"  EnablePageMethods="true"   EnablePartialRendering="true" runat="server">  </asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
   <ContentTemplate>
	<h1 class="h3 mb-3">SENARAI <strong>PENGESAHAN</strong></h1>
	<div class="card">
        <div class="card-body table-responsive">
            <asp:GridView ID="Senarai" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="Senarai_PageIndexChanging" CssClass="table table-striped table-bordered table-hover Grid" OnDataBound="Senarai_DataBound" AllowPaging="True">
                <PagerSettings Mode="Numeric" Position="Bottom" />
                <Columns>
                    <asp:TemplateField HeaderText="ID" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Tajuk" HeaderText="Tajuk" SortExpression="Tajuk">
                        <ItemStyle Wrap="true" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NamaJabatan" HeaderText="Jabatan" SortExpression="Jabatan">
                        <ItemStyle Wrap="true" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="Status_Permohonan" HeaderText="Status" SortExpression="Status">
                                                <ItemStyle  Wrap="true" />
                                            </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="Status">
                        <ItemStyle Width="0.5%" Wrap="true" />
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status_Permohonan") %>'></asp:Label>
                            <asp:Label ID="lblIDStatus" Visible="false" runat="server" Text='<%# Eval("IdStatusPermohonan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan">
                        <ItemStyle  Width="0.5%"  Wrap="true" />
                        <ItemTemplate>
                            <a href="papar-pengesahan.aspx?id=<%# Eval("Id") %>"><img src="/image/check.png" title=""></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
	</div>

</ContentTemplate>    </asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
