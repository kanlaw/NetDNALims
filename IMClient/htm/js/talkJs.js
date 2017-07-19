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
function clearAll()
{
    document.getElementById("mainID").innerHTML = "";
}

//添加用户消息
function add(isSelf, strConent, sendName, sendTime, fontFamily, fontSize, imgHeadUrl) {
    var div = document.createElement("div");
    var p1 = document.createElement("p");
    var p2 = document.createElement("p");
    var a = document.createElement("a");
    var img_head = document.createElement("img");
    var divClass = 'chat_content_group self';
    var imgClass = 'chat_content_avatar';
    var p1Class = 'chat_nick';
    var p2Class = 'chat_content';
    var p1content = sendTime + " " + sendName;
    if (!isSelf) {

        p1content = sendName + " " + sendTime;
        divClass = 'chat_content_group buddy';
    }

    div.setAttribute("class", divClass);
    //img_head.setAttribute("href", imgHeadUrl);
    img_head.src = imgHeadUrl;
    img_head.className = "chat_content_avatar";
    img_head.height = "35px";
    img_head.width = "35px";

    p1.setAttribute("class", p1Class);
    p1.innerHTML = p1content;
    p2.setAttribute("class", p2Class);
    p2.style.fontFamily = fontFamily
    p2.style.fontSize = fontSize;
    p2.innerHTML = strConent;

    //var imglist = p2.getElementsByTagName("img");
    //for (var i = 0; i < imglist.length; i++) {
    //    var imgTmp = imglist[i];
    //    imgTmp.onload = function () {
    //        AutoResizeImage(p2.clientWidth - 30, 0, imgTmp);
    //    }
    //}

    div.appendChild(img_head);
    div.appendChild(p1);
    div.appendChild(p2);
    
    document.body.appendChild(div);
    document.body.appendChild(a);
    document.body.scrollTop = document.body.scrollHeight;
}

//添加系统消息
function addSys(strConent, fontFamily, fontSize,infoType) {
    var div = document.createElement("div");

    var p2 = document.createElement("p");
    var img_head = document.createElement("img");

    var divClass = 'chat_content_group sys';

    var p2Class = 'chat_content';
    
    var imgUrl = "'Source/default/sysImage/tick_small_03.png'";
    switch (infoType)
    {
        case 1://感叹号
            imgUrl = "'Source/default/sysImage/warm_small.png'";
            break;
        case 2://×号
            imgUrl = "'Source/default/sysImage/failure_small.png'";
            break;
        default:
            break;
    }
    var img = "<img style='margin-left:12px;margin-right:6px;' src=" + imgUrl + "/>";

    div.setAttribute("class", divClass);

    p2.setAttribute("class", p2Class);
    div.style.alignContent = "center";
    p2.style.fontFamily = fontFamily
    p2.style.fontSize = fontSize;
    p2.innerHTML = img + strConent;
    div.appendChild(p2);
    document.body.appendChild(div);
    document.body.scrollTop = document.body.scrollHeight;
}

/*
文件发送
fileIcoUrl 文件ico 位置
fileName 文件名称
fileSize 文件大小
fileResult 文件最后操作结果
*/
function addFile(isSelf, strConent, sendName, sendTime, fontFamily, fontSize, imgHeadUrl, fileIcoUrl, fileName, fileDir, fileSize, fileResult
    ,fileNamelimit,fileResultlimit
    ) {
    var div = document.createElement("div");
    var p1 = document.createElement("p");
    var p2 = document.createElement("p");
    var img_head = document.createElement("img");
    var divClass = 'chat_content_group self';
    var imgClass = 'chat_content_avatar';
    var p1Class = 'chat_nick';
    var p2Class = 'chat_content';
    var p1content = sendTime + " " + sendName;
    if (!isSelf) {
        p1content = sendName + " " + sendTime;
        divClass = 'chat_content_group buddy';
    }



    div.setAttribute("class", divClass);
    img_head.src = imgHeadUrl;
    img_head.className = "chat_content_avatar";
    img_head.height = "35px";
    img_head.width = "35px";

    p1.setAttribute("class", p1Class);
    p1.innerHTML = p1content;
    p2.setAttribute("class", p2Class);
    p2.style.fontFamily = fontFamily
    p2.style.fontSize = fontSize;

    var divContent = document.createElement("div");
    var tableContent = document.createElement("table");

    tableContent.className = "tableCss";

    
    var tr = document.createElement("tr");
    var tdHeight = 58;
    var td = document.createElement("td");//保存图片
    td.style = "width:65px;height:" + tdHeight + "px;border-top-width:1px;border-left-width:1px;"
    var canvasContent = document.createElement("canvas");
    canvasContent.width = 65;
    canvasContent.height = tdHeight;
    td.appendChild(canvasContent);
    tr.appendChild(td);

    var td1_2 = document.createElement("td");//保存文字描述
    td1_2.style = "font-size:12px;border-right-width:1px; border-top-width:1px;";

    var divTitle = document.createElement("div");//文件名称 文件大小
    var spanFile = document.createElement("span");
    var spanFileSize = document.createElement("span");

    var tmpfileName = fileName;
    if (tmpfileName.length > fileNamelimit)
    {
        tmpfileName = tmpfileName.substr(0, fileNamelimit)+"...";
    }
    spanFile.textContent = tmpfileName+"   ";
    spanFile.style.fontSize = "12px";
    
    spanFileSize.textContent = fileSize;
    spanFileSize.style.fontSize = "12px";
    spanFileSize.style.color = "#777777";

    divTitle.appendChild(spanFile);
    divTitle.appendChild(spanFileSize);
    //divTitle.style.fontSize = "14px";
    divTitle.style.marginBottom = "6";
    //divTitle.textContent = "title";
    //divTitle.textContent = fileName + "   " + fileSize;
    td1_2.appendChild(divTitle);//文件上传状态/文件保存位置

    var divFilePath = document.createElement("div");//文件操作最后结果
    divFilePath.style.color = "#777777";
    divFilePath.style.fontSize = "13px";
    //divFilePath.textContent = "filePath";
    var tmpfileResult = fileResult;
    if (tmpfileResult.length > fileResultlimit)
    {
        tmpfileResult = tmpfileResult.substr(0, fileResultlimit) + "...";
    }
    divFilePath.textContent = tmpfileResult + "   ";
    td1_2.appendChild(divFilePath);

    tr.appendChild(td1_2);

    tableContent.appendChild(tr);

    var tr2 = document.createElement("tr");
    var td2_1 = document.createElement("td");
    var a1 = document.createElement("a");

    var filePath_All = fileDir + fileName;
    

    a1.style.marginRight = 20;
    a1.style.cursor = "hand";
    a1.onmousemove = function () {
        a1.style.textDecoration = "underline";
    }
    a1.onmouseout = function () {
        a1.style.textDecoration = "none";
    }
    //打开文件
    a1.onclick = function ()
    {
        
        //jsOBJ.JS_OpenFile(filePath_All);
        jsOBJ.JS_OpenFile(strConent);
    }

    var a2 = document.createElement("a");
    a2.style.cursor = "hand";
    a2.onmousemove = function () {
        a2.style.textDecoration = "underline";
    }
    a2.onmouseout = function () {
        a2.style.textDecoration = "none";
    }
    //打开文件目录
    a2.onclick = function ()
    {
        //filePath_All
        jsOBJ.JS_OpenDir(strConent, fileDir);
    }
    a1.text = "打开";
     
    a2.text = "打开文件夹";
    td2_1.colSpan = 2;
    td2_1.height = tableContent.height-td.height;
    td2_1.style = "font-size:13px;color:#009bdb;padding-right:20px; text-align:right;border-width:1px;";
    td2_1.appendChild(a1);
    td2_1.appendChild(a2);
    tr2.appendChild(td2_1);
    tableContent.appendChild(tr2);
    divContent.appendChild(tableContent);
    p2.appendChild(divContent)



    div.appendChild(img_head);
    div.appendChild(p1);
    div.appendChild(p2);

    document.body.appendChild(div);
    document.body.scrollTop = document.body.scrollHeight;
    //fileIcoUrl
    //drawIconF(canvasContent, "Source/yujunming/img/ae2d2f5c9ecc43dab71fb7d279f124b7.jpg");
    drawIconF(canvasContent, fileIcoUrl);
}
function drawIconF(gra, icoUrl) {
    var imgwidth = 40;
    var imgHeight = 40;

    var flagWidth = 14;
    var flagHeight = 14;

    //缩略图
    var ico = new Image();//"Source/default/sysImage/play01.png
    //ico.height=imgHeight;
    //ico.width=imgwidth;

    //完成图标
    var flag = new Image();
    flag.width = flagWidth;
    flag.height = flagHeight;

    //flag.load();
    //flag.onloadstart();

    var g = gra;
    var cxt = g.getContext("2d");


    var imgX = (g.width - imgwidth) / 2;
    var imgY = (g.height - imgHeight) / 2;
    ico.onload = function () {
        cxt.drawImage(ico, 0, 0, ico.width, ico.height, imgX, imgY, imgwidth, imgHeight);
    }
    ico.src = icoUrl;

    /*
    var imgX = (g.width - imgwidth) / 2;
    var imgY = (g.height - imgHeight) / 2;
    var pX = imgX + imgwidth - flag.width / 3 * 2;
    var pY = imgY + imgHeight - flag.height / 3 * 2;
    flag.onload = function () {
        cxt.drawImage(flag, 0, 0, flag.width, flag.height, pX, pY, flag.width, flag.height);
    }
    flag.src = "Source/default/sysImage/tick_small_03.png";
    */
}

function AutoResizeImage(maxWidth, maxHeight, objImg) {

    var hRatio;
    var wRatio;
    var Ratio = 1;
    var w = objImg.width;
    var h = objImg.height;

    wRatio = maxWidth / w;
    hRatio = maxHeight / h;


    if (maxWidth == 0 && maxHeight == 0) {
        Ratio = 1;
    } else if (maxWidth == 0) {//  
        if (hRatio < 1) Ratio = hRatio;
    } else if (maxHeight == 0) {
        if (wRatio < 1) Ratio = wRatio;
    } else if (wRatio < 1 || hRatio < 1) {
        Ratio = (wRatio <= hRatio ? wRatio : hRatio);
    }
    if (Ratio < 1) {
        w = w * Ratio;
        h = h * Ratio;
    }
    objImg.height = h;
    objImg.width = w;

}

function AutoResizeImageForImg(objImg) {
    var maxWidth = objImg.parentNode.clientWidth - 30;
    var maxHeight = 0;
    var hRatio;
    var wRatio;
    var Ratio = 1;
    var w = objImg.width;
    var h = objImg.height;

    wRatio = maxWidth / w;
    hRatio = maxHeight / h;


    if (maxWidth == 0 && maxHeight == 0) {
        Ratio = 1;
    } else if (maxWidth == 0) {//  
        if (hRatio < 1) Ratio = hRatio;
    } else if (maxHeight == 0) {
        if (wRatio < 1) Ratio = wRatio;
    } else if (wRatio < 1 || hRatio < 1) {
        Ratio = (wRatio <= hRatio ? wRatio : hRatio);
    }
    if (Ratio < 1) {
        w = w * Ratio;
        h = h * Ratio;
    }
    objImg.setAttribute("heigth", h + "px");
    objImg.setAttribute("width", w + "px");
    //objImg.height=h ;
    //objImg.width = w;

}

function showPic_html(objImg)
{
    //alert("ok");
    //alert(jsOBJ);
    //alert(objImg.src);
    var imgPath = jsOBJ.JS_localPathByHtmlPath(objImg.src);
    jsOBJ.OpenImage(imgPath); 
}

function showPic_html_Remote(objImg) {
    //alert("ok");
    //alert(jsOBJ);
    //alert(objImg.src);
    //var imgPath = jsOBJ.retlocalPathByHtmlPath(objImg.src);
    jsOBJ.OpenRemoteImage(objImg.src);
}

var playStr = "Source/default/sysImage/play01.png";
var StopStr = "Source/default/sysImage/voice.gif";
//声音
//添加声音播放元素，独立 不含文字信息 addVoice(true,'b463804ab27449939ab210d7dca60a89', 'chengle', '2017-03-16 15:16:11', '宋体','9px','source/chengle/Head/default.jpg',' http://192.168.1.88:1111/voice/b463804ab27449939ab210d7dca60a89.wav')
function addVoice(isSelf, audioId, sendName, sendTime,
         fontFamily, fontSize, imgHeadUrl, voiceUrl) {

    //alter('0');
    var div = document.createElement("div");
    var p1 = document.createElement("p");
    var p2 = document.createElement("p");
    var a = document.createElement("a");
    var img_head = document.createElement("img");

    var audio = document.createElement("audio");
    //var a_playFlag = document.createElement("a");
    var img_playFlag = document.createElement("img");

    var divClass = 'chat_content_group self';
    var imgClass = 'chat_content_avatar';
    var p1Class = 'chat_nick';
    var p2Class = 'chat_content';
    var p1content = sendTime + " " + sendName;
    if (!isSelf) {
        //p1content = 2;
        p1content = sendName + " " + sendTime;
        divClass = 'chat_content_group buddy';
    }

    div.setAttribute("class", divClass);
    //img_head.setAttribute("href", imgHeadUrl);
    img_head.src = imgHeadUrl;
    img_head.className = "chat_content_avatar";
    img_head.height = "35px";
    img_head.width = "35px";

    p1.setAttribute("class", p1Class);
    p1.innerHTML = p1content;
    p2.setAttribute("class", p2Class);
    p2.style.fontFamily = fontFamily
    p2.style.fontSize = fontSize;
    //alter('1');

    audio.src = voiceUrl;
    audio.id = audioId;
    img_playFlag.width = 18;
    img_playFlag.height = 18;
    img_playFlag.style.cursor = "hand";
    //audio.controls = "controls";
    audio.onended = function () {
        img_playFlag.src = playStr;
        //a_playFlag.text = "播放";
    }
    //alter('2');
    img_playFlag.src = playStr;
    //a_playFlag.text = "播放";
    //a_playFlag.onclick = function () {
    //    playOrPausedByObj(audio, a_playFlag);
    //}
    img_playFlag.onclick = function () {
        playOrPausedByObj(audio, img_playFlag);
    }
    //alter('3');
    //p2.appendChild(a_playFlag);
    p2.appendChild(img_playFlag);

    p2.appendChild(audio);
    //p2.innerHTML = strConent;





    div.appendChild(img_head);
    div.appendChild(p1);
    div.appendChild(p2);

    document.body.appendChild(div);
    document.body.appendChild(a);
    document.body.scrollTop = document.body.scrollHeight;

    //预加载
    audio.load();
}


//获取播放时间
function getCurrentTime(id) {
    var audio = document.getElementById(id);
    alert(parseInt(audio.currentTime) + '：秒');
}

//播放
function playOrPausedByObj(audio, obj) {
    if (audio.paused) {
        audio.play();
        obj.src = StopStr;
        return;
    }
    audio.pause();
    audio.currentTime = 0;
    obj.src = playStr;
}

//播放
function playOrPaused(id, obj) {
    var audio = document.getElementById(id);
    if (audio.paused) {
        audio.play();
        obj.innerHTML = '暂停';
        return;
    }
    audio.pause();
    obj.innerHTML = '播放';
}

//播放器显示隐藏
function hideOrShowControls(id, obj) {
    var audio = document.getElementById(id);
    if (audio.controls) {
        audio.removeAttribute('controls');
        obj.innerHTML = '显示控制框'
        return;
    }
    audio.controls = 'controls';
    obj.innerHTML = '隐藏控制框'
    return;
}

//声音
function vol(id, type, obj) {
    var audio = document.getElementById(id);
    if (type == 'up') {
        var volume = audio.volume + 0.1;
        if (volume >= 1) {
            volume = 1;

        }
        audio.volume = volume;
    } else if (type == 'down') {
        var volume = audio.volume - 0.1;
        if (volume <= 0) {
            volume = 0;
        }
        audio.volume = volume;
    }
    document.getElementById('nowVol').innerHTML = returnFloat1(audio.volume);
}

//是否静音
function muted(id, obj) {
    var audio = document.getElementById(id);
    if (audio.muted) {
        audio.muted = false;
        obj.innerHTML = '开启静音';
    } else {
        audio.muted = true;
        obj.innerHTML = '关闭静音';
    }
}

//保留一位小数点
function returnFloat1(value) {
    value = Math.round(parseFloat(value) * 10) / 10;
    if (value.toString().indexOf(".") < 0) {
        value = value.toString() + ".0";
    }
    return value;
}

//间隔线 
function addHistoryLine(DateStr)
{
    var div = document.createElement("div");
    var divline1 = document.createElement("div");
    var divline2 = document.createElement("div");
    var divDate1 = document.createElement("div");

    var hr1 = document.createElement("hr");
    var hr2 = document.createElement("hr");
    hr1.className = "hrLine";
    hr2.className = "hrLine";

    divline1.className = "divline";
    divline2.className = "divline";
    
    divDate1.className = "divlineContent";
    divDate1.textContent = DateStr;

    divline1.appendChild(hr1);
    divline2.appendChild(hr2);

     div.appendChild(divline1);
     div.appendChild(divDate1);
     div.appendChild(divline2);
    document.body.appendChild(div);
}