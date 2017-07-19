$(window).resize(function () {

    var plist = document.getElementsByClassName("chat_content");

    for (var pindex = 0; pindex < plist.length; pindex++) {
        var p2 = plist[pindex];
        var imglist = p2.getElementsByTagName("img");
        for (var i = 0; i < imglist.length; i++) {
            var imgTmp = imglist[i];
            AutoResizeImage(p2.clientWidth - 30, 0, imgTmp);
        }
    }

});
//清空浏览器中内容
function clearAll() {
    document.getElementById("mainID").innerHTML = "";
}

function setDivHeight(allHeight)
{
    var parent1Div = document.getElementById("divEmcee");
    parent1Div.clientHeight = allHeight / 2;
    var parent2Div = document.getElementById("divActor");
    parent2Div.clientHeight = allHeight / 2;
}
//添加用户消息

function addMeeting(isSelf, strConent, sendName, sendTime, fontFamily, fontSize, imgHeadUrl, isEmcee) {

    var parentDiv = document.getElementById("divEmcee");
    if (!isEmcee) {
        parentDiv = document.getElementById("divActor");
    }

    var div = document.createElement("div");
    var p1 = document.createElement("p");
    var p2 = document.createElement("p");
    var a = document.createElement("a");

    var divClass = 'chat_content_group self';
    var imgClass = 'chat_content_avatar';
    var p1Class = 'chat_nick';
    var p2Class = 'chat_content';
    var p1content = sendTime + " " + sendName;
    if (isEmcee) {
        divClass = 'chat_content_group Emecc';
        p2Class = 'chat_content_Emcee';
        p1content = sendTime;
    }
    else {
        if (!isSelf) {
            p1content = sendName + " " + sendTime;
            divClass = 'chat_content_group buddy';
        }
    }


    div.setAttribute("class", divClass);
    p1.setAttribute("class", p1Class);
    p1.innerHTML = p1content;
    p2.setAttribute("class", p2Class);
    p2.style.fontFamily = fontFamily
    p2.style.fontSize = fontSize;
    p2.innerHTML = strConent;




    if (!isEmcee) {
        var img_head = document.createElement("img");
        img_head.src = imgHeadUrl;
        img_head.className = "chat_content_avatar";
        img_head.height = "35px";
        img_head.width = "35px";
        div.appendChild(img_head);
    }
    div.appendChild(p1);
    div.appendChild(p2);

    parentDiv.appendChild(div);
    parentDiv.scrollTop = parentDiv.scrollHeight;
}

function addMeeting_2(isSelf, strConent, sendName, sendTime, fontFamily, fontSize, imgHeadUrl, isEmcee) {

    var parentDiv = document.body;
  

    var div = document.createElement("div");
    var p1 = document.createElement("p");
    var p2 = document.createElement("p");
    var a = document.createElement("a");

    var divClass = 'chat_content_group self';
    var imgClass = 'chat_content_avatar';
    var p1Class = 'chat_nick';
    var p2Class = 'chat_content';
    var p1content = sendTime + " " + sendName;
    if (isEmcee) {
        divClass = 'chat_content_group Emecc';
        p2Class = 'chat_content_Emcee';
        p1content = sendTime;
    }
    else {
        if (!isSelf) {
            p1content = sendName + " " + sendTime;
            divClass = 'chat_content_group buddy';
        }
    }


    div.setAttribute("class", divClass);
    //div.style.textAlign = "left";
    p1.setAttribute("class", p1Class);
    //p1.style.textAlign = "left";
    p1.innerHTML = p1content;
    p2.setAttribute("class", p2Class);
    p2.style.fontFamily = fontFamily
    p2.style.fontSize = fontSize+"px";
    p2.innerHTML = strConent;




    if (!isEmcee) {
        var img_head = document.createElement("img");
        img_head.src = imgHeadUrl;
        img_head.className = "chat_content_avatar";
        img_head.height = "35px";
        img_head.width = "35px";
        div.appendChild(img_head);
    }
    div.appendChild(p1);
    div.appendChild(p2);

    parentDiv.appendChild(div);
    parentDiv.scrollTop = parentDiv.scrollHeight;
}