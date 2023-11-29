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
                    <asp:TemplateField HeaderText="ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NO.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>
              
                    <asp:TemplateField HeaderText="TAJUK" SortExpression="Tajuk">
                        <ItemStyle Width="50%" Wrap="true" />
                        <ItemTemplate>
                            <asp:Label ID="lblTajukUtama" runat="server" Text='<%# Eval("Tajuk") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NamaJabatan" HeaderText="KEMENTERIAN / JABATAN" SortExpression="Jabatan">
                        <ItemStyle Wrap="true" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Harga" HeaderText="HARGA" DataFormatString="RM {0:n}" SortExpression="Harga">
                        <ItemStyle Width="12%" Wrap="true" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="STATUS">
                        <ItemStyle Width="0.5%" Wrap="true" />
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status_Permohonan") %>'></asp:Label>
                            <asp:Label ID="lblIDStatus" Visible="false" runat="server" Text='<%# Eval("IdStatusPermohonan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="TINDAKAN">
                        <ItemStyle  Width="0.5%"  Wrap="true" />
                        <ItemTemplate>
                            <a href="papar-pengesahan.aspx?id=<%# Eval("Id") %>"><img src="/image/check.png" title=""></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

                <asp:Panel ID="Panel1" Visible="true" runat="server">
                       <div class="btn-group btn-group-sm mb-3 float-end" role="group">

                           <div class="form-group">

                               <asp:Button ID="btnExcel" runat="server" OnClick="ExportToExcel" CssClass="btn btn-primary " Text="Export Excel" />
                               <asp:Button ID="btnPdf" runat="server" OnClick="ExportToPDF" CssClass="btn btn-primary" Text="Export PDF" />

                           </div>

                       </div>
                   </asp:Panel>


        </div>
	</div>

</ContentTemplate>    </asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
