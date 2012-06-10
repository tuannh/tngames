<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-GameList.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_GameList" %>
<div class="gameList" style="text-align: center; padding-top: 15px; padding-right: 3px;">
    <div id="allgame">
        <% if (!IsPausedPrediction)
           {  %>
        <p>
            <a class="dudoan" href="javascript:void(0);" title="Thử tài đự doán">
                <img alt="dudoan" src="/images/game_dudoan.png" />
            </a>
        </p>
        <%} %>
        <% if (!IsPausedQuestion)
           {%>
        <p>
            <a class="cauhoi" href="javascript:void(0);" title="Thử tài kiến thức">
                <img alt="cauhoi" src="/images/game_cauhoi.png" />
            </a>
        </p>
        <%} %>
        <% if (!IsPausedBetting)
           {%>
        <p>
            <a class="cacuoc" href="javascript:void(0);" title="Thử tài phân tích trận đấu">
                <img alt="cacuoc" src="/images/game_cacuoc.png" />
            </a>
        </p>
        <%} %>
    </div>
    <% if (!IsPausedBetting)
       {  %>
    <div id="cacuoc" class="invisible">
        <img alt="cacuoc" src="/images/game_cacuoc_PLAY.png" />
        <a href="/tro-choi/thu-tai-phan-tich-tran-dau" title="Thử tài phân tích trận đấu">Play</a>
        <a href="javascript:void(0);" class="btnXclose">Close</a>
    </div>
    <%} %>
    <% if (!IsPausedQuestion)
       {%>
    <div id="cauhoi" class="invisible">
        <img alt="cauhoi" src="/images/game_cauhoi_play.png" />
        <a href="/tro-choi/thu-tai-kien-thuc" title="Thử tài kiến thức">Play </a><a href="javascript:void(0);"
            class="btnXclose">Close</a>
    </div>
    <%} %>
    <% if (!IsPausedPrediction)
       {%>
    <div id="dudoan" class="invisible">
        <img alt="dudoan" src="/images/game_dudoan_play.png" />
        <a href="/tro-choi/thu-tai-du-doan" title="Thử tài đự doán">Play</a> <a href="javascript:void(0);"
            class="btnXclose">Close</a>
    </div>
    <%} %>
</div>
<script type="text/javascript">
//<![CDATA[
    $('#allgame a').click(function () {
        var id = '#' + $(this).attr('class');
        $('#allgame').animate({ opacity: 0.0 }, 500, function () {
            $(this).addClass("invisible");

            $(id).css("opacity", 0.0)
                 .removeClass("invisible")
                 .animate({ opacity: 1.0 }, 500);
        });
    })

    $('.gameList .btnXclose').click(function () {
        $(this).parent().animate({ opacity: 0.0 }, 500, function () {
            $(this).addClass("invisible");

            $('#allgame').css("opacity", 0.0)
                 .removeClass("invisible")
                 .animate({ opacity: 1.0 }, 500);
        });
    })

//]]>
</script>
