<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BettingEdit.ascx.cs"
    Inherits="TNGames.Controls.Admin.BettingEdit" %>
<asp:Panel ID="pnlInfo" runat="server">
    <h1 class="header">
        Thêm/sửa</h1>
    <asp:Label ID="lblMsg" runat="server" />
    <table width="100%">
        <tr>
            <td class="contentbold" style="width: 150px;">
                Trận đấu
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" MaxLength="150" CssClass="text" />
                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="contentbold" valign="top">
                Nội dung
            </td>
            <td>
                <asp:TextBox ID="txtDesc" runat="server" CssClass="textarea" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table style="width: 500px">
                    <tr>
                        <td class="contentbold" style="text-align: left;">
                            Tên đội A
                        </td>
                        <td class="contentbold" style="text-align: left;">
                            Tên đội B
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Tên đội
            </td>
            <td>
                <table style="width: 500px">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtHomeTeam" runat="server" Width="222px" MaxLength="150" />
                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator2" runat="server"
                                ErrorMessage="*" ControlToValidate="txtHomeTeam"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVisitingTeam" runat="server" Width="222px" MaxLength="150" />
                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator3" runat="server"
                                ErrorMessage="*" ControlToValidate="txtVisitingTeam"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Tỷ lệ
            </td>
            <td>
                <table style="width: 500px">
                    <tr>
                        <td colspan="2">
                            <asp:Repeater ID="rptRate" ClientIDMode="Predictable" runat="server" OnItemCommand="rptRate_ItemCommand"
                                OnItemDataBound="rptRate_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="rateList" width="100%">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:DropDownList ID="ddlHomeN" CssClass="home n" runat="server" ValidationGroup="add">
                                                <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlHome" CssClass="home m" runat="server">
                                                <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                                <asp:ListItem Value="0.25" Text="1/4"></asp:ListItem>
                                                <asp:ListItem Value="0.5" Text="1/2"></asp:ListItem>
                                                <asp:ListItem Value="0.75" Text="3/4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 50%">
                                            <asp:DropDownList ID="ddlVisitingN" CssClass="visit n" runat="server" ValidationGroup="add">
                                                <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlVisiting" CssClass="visit m" runat="server">
                                                <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                                <asp:ListItem Value="0.25" Text="1/4"></asp:ListItem>
                                                <asp:ListItem Value="0.5" Text="1/2"></asp:ListItem>
                                                <asp:ListItem Value="0.75" Text="3/4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="display: none;">
                                            <asp:TextBox ID="txtOrder" Width="50px" runat="server" MaxLength="3" Text='<%# Eval("Order") %>' />
                                            <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                TargetControlID="txtOrder" />
                                        </td>
                                        <td style="display: none;">
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument="<%# Container.ItemIndex %>"
                                                CommandName="delete" OnClientClick="return confirm('Bạn muốn xóa tỷ lệ ra khỏi danh sách không?\nNhấn nút lưu để để lưu lại dữ liệu.')" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trScore" runat="server" visible="false">
            <td class="contentbold">
                Tỷ số
            </td>
            <td>
                <table style="width: 500px">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtHomeGoalScore" runat="server" CssClass="textS" MaxLength="150" />
                            <asp:RequiredFieldValidator ID="rvf2" runat="server" ForeColor="Red" ErrorMessage="*"
                                ControlToValidate="txtHomeGoalScore" />
                            <act:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="txtHomeGoalScore" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtVisitingGoalScore" runat="server" CssClass="textS" MaxLength="150" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                ErrorMessage="*" ControlToValidate="txtVisitingGoalScore" />
                            <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                TargetControlID="txtVisitingGoalScore" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="display: none;">
            <td class="contentbold" valign="top">
            </td>
            <td>
                <asp:Panel ID="pnlAdd" runat="server">
                    <h3>
                        Thêm tỷ lệ</h3>
                    <table class="addContainer">
                        <tr>
                            <td>
                                Tỷ lệ đội nhà
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlHomeN" CssClass="home n" runat="server" ValidationGroup="add">
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlHome" CssClass="home m" runat="server" ValidationGroup="add">
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="0.25" Text="1/4"></asp:ListItem>
                                    <asp:ListItem Value="0.5" Text="1/2"></asp:ListItem>
                                    <asp:ListItem Value="0.75" Text="3/4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tỷ lệ đội khách
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVisitingN" CssClass="visit n" runat="server" ValidationGroup="add">
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlVisiting" CssClass="visit m" runat="server" ValidationGroup="add">
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="0.25" Text="1/4"></asp:ListItem>
                                    <asp:ListItem Value="0.5" Text="1/2"></asp:ListItem>
                                    <asp:ListItem Value="0.75" Text="3/4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Thứ tự
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrder" runat="server" MaxLength="3" Text="0" Width="50px" />
                                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                    TargetControlID="txtOrder" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Text="Thêm tỷ lệ" OnClick="btnAdd_Click" ValidationGroup="add" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Ngày giờ thi đấu
            </td>
            <td>
                <asp:TextBox ID="txtPlayDate" runat="server" MaxLength="150" CssClass="textS" />
                <asp:RequiredFieldValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                    ControlToValidate="txtPlayDate" ForeColor="Red"></asp:RequiredFieldValidator>
                <act:MaskedEditExtender runat="server" MaskType="Date" Mask="99/99/9999" TargetControlID="txtPlayDate"
                    CultureName="en-US" />
                <act:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPlayDate" />
                <asp:TextBox ID="txtPlayTime" runat="server" MaxLength="150" Width="60px" />
                <act:MaskedEditExtender ID="MaskedEditExtender3" runat="server" MaskType="Time" Mask="99:99"
                    AcceptAMPM="true" TargetControlID="txtPlayTime" CultureName="en-US" />
                <asp:RequiredFieldValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                    ControlToValidate="txtPlayTime" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="display: none;">
            <td class="contentbold">
                Trạng thái
            </td>
            <td>
                <label>
                    <asp:RadioButton ID="radYes" runat="server" ValidationGroup="Active" />
                    Hiển thị
                </label>
                <label>
                    <asp:RadioButton ID="radNo" runat="server" Checked="true" ValidationGroup="Active" />
                    Ẩn
                </label>
            </td>
        </tr>
        <tr style="display: none;">
            <td class="contentbold">
                Ngày bắt đầu
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" MaxLength="150" CssClass="textS" />
                <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Date" Mask="99/99/9999"
                    TargetControlID="txtStartDate" CultureName="en-US" />
                <act:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate" />
            </td>
        </tr>
        <tr style="display: none;">
            <td class="contentbold">
                Ngày kết thúc
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server" MaxLength="150" CssClass="textS" />
                <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" MaskType="Date" Mask="99/99/9999"
                    TargetControlID="txtEndDate" CultureName="en-US" />
                <act:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" OnClientClick="return confirm('Bạn muốn xóa trận đấu này không?\nLưu ý: Tất cả thông tin người chơi liên quan sẽ bị xóa.')"
                    OnClick="btnDelete_Click" />
                <input type="button" value="Thoát" onclick="location.href='/admincp/betting-list/'" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
    //<![CDATA[
        $('.rateList .home, .rateList .visit').change(function () {
            trParent = $(this).parent().parent();
            if ($(this).hasClass('home')) {
                n = trParent.find('.home.n').val();
                m = trParent.find('.home.m').val();

                if (n != 0 || m != 0) {
                    trParent.find('.visit.n').val(0);
                    trParent.find('.visit.m').val(0);
                }
            }
            else {
                n = trParent.find('.visit.n').val();
                m = trParent.find('.visit.m').val();

                if (n != 0 || m != 0) {
                    trParent.find('.home.n').val(0);
                    trParent.find('.home.m').val(0);
                }
            }
        })

        // add panel
        $('.addContainer .home, .addContainer .visit').change(function () {
            if ($(this).hasClass('home')) {
                n = $('.addContainer .home.n').val();
                m = $('.addContainer .home.m').val();

                if (n != 0 || m != 0) {
                    $('.addContainer .visit.n').val(0);
                    $('.addContainer .visit.m').val(0);
                }
            }
            else {
                n = $('.addContainer .visit.n').val();
                m = $('.addContainer .visit.m').val();

                if (n != 0 || m != 0) {
                    $('.addContainer .home.n').val(0);
                    $('.addContainer .home.m').val(0);
                }
            }
        })

        $('form').submit(function () {
            hn = $('.rateList .home.n').val();
            hm = $('.rateList .home.m').val();

            vn = $('.rateList .visit.n').val();
            vm = $('.rateList .visit.m').val();

            if (hn == '0' && hm == '0' && vn == '0' && vm == '0') {
                alert("Bạn chưa chọn tỷ lệ");
                return false;
            }

            return true;
        });

    //]]>
    </script>
</asp:Panel>
<asp:Panel ID="pnlScore" runat="server" CssClass="containTable">
    <h1 class="header">
        Cập nhật tỷ số</h1>
    <asp:Label ID="lbMsg2" runat="server" />
    <table width="100%">
        <tr>
            <td>
            </td>
            <td>
                <table width="324px">
                    <tr>
                        <td style="width: 50%;">
                            <b>
                                <asp:Literal ID="litHome" runat="server" />
                            </b>
                        </td>
                        <td>
                            <b>
                                <asp:Literal ID="litVisiting" runat="server" />
                            </b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Tỷ số
            </td>
            <td>
                <table width="324px">
                    <tr>
                        <td style="width: 50%;">
                            <asp:TextBox ID="txtHomeScore" runat="server" ValidationGroup="score" />
                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator6" runat="server"
                                ValidationGroup="score" ErrorMessage="*" ControlToValidate="txtHomeScore" />
                            <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                TargetControlID="txtHomeScore" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtVisitngScore" runat="server" ValidationGroup="score" />
                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator7" runat="server"
                                ValidationGroup="score" ErrorMessage="*" ControlToValidate="txtVisitngScore" />
                            <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                TargetControlID="txtVisitngScore" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <br />
                <asp:Button ID="btnSaveScore" runat="server" ValidationGroup="score" Text="Cập nhật"
                    OnClick="btnSaveScore_Click" />
                <input type="button" value="Thoát" onclick="location.href='/admincp/betting-list/'" />
            </td>
        </tr>
    </table>
</asp:Panel>
