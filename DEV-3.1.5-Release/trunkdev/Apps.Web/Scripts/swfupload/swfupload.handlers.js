/* ******************************************
*	初始化SWFUpload上传控件
* ****************************************** */
function InitSWFUploadByExcel(upurl, uppath, upsize, flashurl) {
    var sendUrl = upurl + "?action=ExcelFile&UpFilePath=" + uppath;
    var swfu = new SWFUpload({
        // Backend Settings
        upload_url: sendUrl,
        file_post_name: uppath,
        post_params: { "ASPSESSID": "NONE" },

        file_size_limit: upsize, // 2MB
        file_types: "*.xls;*.xlsx;",
        file_types_description: "Excel File",
        file_upload_limit: "1",
        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,

        // Button Settings
        button_placeholder_id: "upload",
        button_width: 50,
        button_height: 23,
        button_text: '浏览…',
        button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
        button_cursor: SWFUpload.CURSOR.HAND,

        // Flash Settings
        flash_url: flashurl,

        custom_settings: {
            upload_target: "show"
        },
        // Debug Settings
        debug: false
    });
}

/* ******************************************
*	初始化SWFUpload上传控件path:/或/xxx/xxx/
* ****************************************** */
function InitSWFUpload(upurl, uppath, upsize, flashurl, iswater, isthumbnail, path) {

    var sendUrl = upurl + "?action=MultipleFile&UpFilePath=" + uppath;
    if (arguments.length == 5) {
        sendUrl = upurl + "?action=MultipleFile&UpFilePath=" + uppath + "&IsWater=" + iswater;
    }
    if (arguments.length == 6) {
        sendUrl = upurl + "?action=MultipleFile&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //带自定义上传路径的URL
    if (arguments.length == 7) {
        sendUrl = upurl + "?action=MultipleFile&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail + "&Path=" + path;
    }
    var swfu = new SWFUpload({
        // Backend Settings
        upload_url: sendUrl,
        file_post_name: uppath,
        post_params: { "ASPSESSID": "NONE" },

        file_size_limit: upsize, // 2MB
        file_types: "*.jpg;*.jpge;*.png;*.gif",
        file_types_description: "JPG Images",
        file_upload_limit: "0",
        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,

        // Button Settings
        button_placeholder_id: "upload",
        button_width: 50,
        button_height: 23,
        button_text: '浏览…',
        button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
        button_cursor: SWFUpload.CURSOR.HAND,

        // Flash Settings
        flash_url: flashurl,

        custom_settings: {
            upload_target: "show"
        },
        // Debug Settings
        debug: false
    });
}


/* ******************************************
*	初始化SWFUpload上传控件path:/或/xxx/xxx/
* ****************************************** */
function InitSWFUploadByFile(upurl, uppath, upsize, flashurl) {

    var sendUrl = upurl + "?action=SingleFile&UpFilePath=" + uppath;
    var swfu = new SWFUpload({
        // Backend Settings
        upload_url: sendUrl,
        file_post_name: uppath,
        post_params: { "ASPSESSID": "NONE" },

        file_size_limit: upsize, // 2MB
        file_types: "*",
        file_types_description: "all files",
        file_upload_limit: "100",
        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,

        // Button Settings
        button_placeholder_id: "upload",
        button_width: 50,
        button_height: 23,
        button_text: '浏览…',
        button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
        button_cursor: SWFUpload.CURSOR.HAND,

        // Flash Settings
        flash_url: flashurl,

        custom_settings: {
            upload_target: "show"
        },
        // Debug Settings
        debug: false
    });
}


/* ******************************************
*	返回上传的状态
* ****************************************** */
function uploadSuccess(file, serverData) {
    try {
        var progress = new FileProgress(file, this.customSettings.upload_target);
        var jsonstr = eval('(' + serverData + ')');
        if (jsonstr.msg == '0') {
            progress.setStatus(jsonstr.msgbox);
            progress.toggleCancel(false);
        }
        if (jsonstr.msg == '1') {
            var imgArr = jsonstr.msgbox.split(",");
            if (file.type == ".jpg" || file.type == ".png" || file.type == ".jpeg" || file.type == ".bmp" || file.type == ".gif") {
                if (imgArr.length == 2) {
                    addImage(imgArr[0], imgArr[1]);
                } else {
                    addImage(jsonstr.msgbox, jsonstr.msgbox);
                }
            } else {
               
                if (imgArr.length == 2) {
                    addFile(imgArr[0], imgArr[1]);
                } else {
                    addFile(jsonstr.msgbox, jsonstr.msgbox);
                }
            }
            progress.setStatus("缩略图创建成功.");
            progress.toggleCancel(false);
        }
    } catch (ex) {
        this.debug(ex);
    }
}

/* ******************************************
*	添加文件列表
* ****************************************** */
function addFile(bigSrc, smallSrc) {

    var focus_photo = $('#focus_photo').val();
    var newLi = $('<div class="add-img-li" style="overflow:hidden;height:20px;"></div>');
    var claname = '';
    if (focus_photo == '') {
        $('#focus_photo').val(smallSrc);
        claname = 'current';
    }
    var newImg = $('<span class="k_fileName">' + bigSrc + '</span>').appendTo(newLi);
    var downa = $('<i onclick="del_img(this);" class="fa fa-trash" style="margin-right:20px;color:#ffa700;display:inline-block;font-style:normal;cursor:pointer"></i>').prependTo(newLi);
    newLi.appendTo("#show_list");
}

/* ******************************************
*	添加图片列表
* ****************************************** */
function addImage(bigSrc, smallSrc) {
    var focus_photo = $('#focus_photo').val();
    var newLi = $('<li style="float:left;border:1px solid #ccc; margin:2px;padding:6px;"></li>');
    var claname = '';
    if (focus_photo == '') {
        $('#focus_photo').val(smallSrc);
        claname = 'current';
    }
    var hidValue = $('<input type="hidden" name="hide_photo_name" value="'+ smallSrc + '" />').appendTo(newLi);
    var trueValue = $('<input type="hidden" name="true_photo_name" value="' + bigSrc + '" />').appendTo(newLi);
    var hidRemark = $('<input type="hidden" name="hide_photo_remark" value="" />').appendTo(newLi);
    var newImg = $('<div onclick="focus_img(this);" class=\"img_box ' + claname + '">' +
        '<img style="max-height:80px;max-width:80px;" src="' + smallSrc + '" bigsrc="' + bigSrc + '" />' +
        '<br/></div>').appendTo(newLi);
 
    var downa = $('<a style="padding:5px;" class="btns" href="javascript:;" onclick="del_img(this);">删除</a>').appendTo(newLi);
    newLi.appendTo("#show_list ul");
    //var list = "";
    //$("#show_list ul li input[name='true_photo_name']").each(function (i) {
    //    if (list == "") {
    //        list = $(this).val();
    //    } else {
    //        list +=","+ $(this).val();
    //    }
    //});

}

/* ******************************************
*	删除LI元素
* ****************************************** */
function del_img(obj) {
    var focusphoto = $("#focus_photo");
    var smallimg = $(obj).prevAll(".img_box").children("img").eq(0).attr("src");
    var node = $(obj).parent(); //要删除的LI节点
    node.remove(); //删除DOM元素
    //检查是否为封面
    if (focusphoto.val() == smallimg) {
        focusphoto.val("");
        var firtimg = $("#show_list ul li .img_box img").eq(0);
        firtimg.parent().addClass("current"); //取第一张做为封面
        focusphoto.val(firtimg.attr("src")); //重新给封面的隐藏域赋值
    }
}

/* ******************************************
*	设置相册封面
* ****************************************** */
function focus_img(obj) {
    $("#focus_photo").val($(obj).children("img").eq(0).attr("src"));
    $("#show_list ul li .img_box").removeClass("current");
    $(obj).addClass("current");
}

/* ******************************************
*	显示图片链接
* ****************************************** */
function show_remark(obj) {
    //取得隐藏值
    var hidRemark = $(obj).prevAll("input[name='hide_photo_remark']").eq(0);

    $.messager.prompt('图片描述', '批量设置请在最前面输入：@', function (r) {
        if (r) {
            if (r.indexOf("@") > -1) {
                r=r.replace("@","");
                $("input[name='hide_photo_remark']").val(r);
                $(".img_box").find(".remark i").html(r);
            } else {
                $(hidRemark).val(r);
                $(obj).prevAll(".img_box").find(".remark i").html(r);
            }
        }
    });

  
}

function fileQueueError(file, errorCode, message) {
	try {
		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setError();
		progress.toggleCancel(false);
		if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
			errorName = "您选择的文件太多.";
		}
		if (errorName !== "") {
			alert(errorName);
			return;
		}

		switch (errorCode) {
		case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
			progress.setStatus(file.name+"文件太小");
			progress.toggleCancel(false, this);
			break;
		case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
			progress.setStatus(file.name+"文件太大");
			progress.toggleCancel(false, this);
			break;
		case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
			progress.setStatus(file.name+"文件类型出错");
			progress.toggleCancel(false, this);
			break;
		default:
			if (file !== null) {
				progress.setStatus("未知错误");
				progress.toggleCancel(false, this);
			}
			break;
		}

	} catch (ex) {
		this.debug(ex);
	}

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
	try {
		if (numFilesQueued > 0) {
			this.startUpload();
		}
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadProgress(file, bytesLoaded) {

	try {
		var percent = Math.ceil((bytesLoaded / file.size) * 100);

		var progress = new FileProgress(file,  this.customSettings.upload_target);
		progress.setProgress(percent);
		if (percent === 100) {
			progress.setStatus("创建缩略图...");
			progress.toggleCancel(false, this);
		} else {
			progress.setStatus("上传...");
			progress.toggleCancel(true, this);
		}
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadComplete(file) {
	try {
		/*  I want the next upload to continue automatically so I'll call startUpload here */
		if (this.getStats().files_queued > 0) {
			this.startUpload();
		} else {
			var progress = new FileProgress(file,  this.customSettings.upload_target);
			progress.setComplete();
			progress.setStatus("上传完成.");
			progress.toggleCancel(false);
		}
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadError(file, errorCode, message) {
	var imageName =  "error.gif";
	var progress;
	try {
		switch (errorCode) {
		case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
			try {
				progress = new FileProgress(file,  this.customSettings.upload_target);
				progress.setCancelled();
				progress.setStatus("Cancelled");
				progress.toggleCancel(false);
			}
			catch (ex1) {
				this.debug(ex1);
			}
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
			try {
				progress = new FileProgress(file,  this.customSettings.upload_target);
				progress.setCancelled();
				progress.setStatus("Stopped");
				progress.toggleCancel(true);
			}
			catch (ex2) {
				this.debug(ex2);
			}
		case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
			imageName = "uploadlimit.gif";
			break;
		default:
			alert(message);
			break;
		}
		addImage("images/" + imageName);
	} catch (ex3) {
		this.debug(ex3);
	}
}

function fadeIn(element, opacity) {
	var reduceOpacityBy = 5;
	var rate = 30;	// 15 fps


	if (opacity < 100) {
		opacity += reduceOpacityBy;
		if (opacity > 100) {
			opacity = 100;
		}

		if (element.filters) {
			try {
				element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
			} catch (e) {
				// If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
				element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
			}
		} else {
			element.style.opacity = opacity / 100;
		}
	}

	if (opacity < 100) {
		setTimeout(function () {
			fadeIn(element, opacity);
		}, rate);
	}
}

/* ******************************************
 *	FileProgress Object
 *	Control object for displaying file info
 * ****************************************** */

function FileProgress(file, targetID) {
	this.fileProgressID = "divFileProgress";

	this.fileProgressWrapper = document.getElementById(this.fileProgressID);
	if (!this.fileProgressWrapper) {
		this.fileProgressWrapper = document.createElement("div");
		this.fileProgressWrapper.className = "progressWrapper";
		this.fileProgressWrapper.id = this.fileProgressID;

		this.fileProgressElement = document.createElement("div");
		this.fileProgressElement.className = "progressContainer";

		var progressCancel = document.createElement("a");
		progressCancel.className = "progressCancel";
		progressCancel.href = "#";
		progressCancel.style.visibility = "hidden";
		progressCancel.appendChild(document.createTextNode(" "));

		var progressText = document.createElement("div");
		progressText.className = "progressName";
		progressText.appendChild(document.createTextNode(file.name));

		var progressBar = document.createElement("div");
		progressBar.className = "progressBarInProgress";

		var progressStatus = document.createElement("div");
		progressStatus.className = "progressBarStatus";
		progressStatus.innerHTML = "&nbsp;";

		this.fileProgressElement.appendChild(progressCancel);
		this.fileProgressElement.appendChild(progressText);
		this.fileProgressElement.appendChild(progressStatus);
		this.fileProgressElement.appendChild(progressBar);

		this.fileProgressWrapper.appendChild(this.fileProgressElement);

		document.getElementById(targetID).appendChild(this.fileProgressWrapper);
		fadeIn(this.fileProgressWrapper, 0);

	} else {
		this.fileProgressElement = this.fileProgressWrapper.firstChild;
		this.fileProgressElement.childNodes[1].firstChild.nodeValue = file.name;
	}

	this.height = this.fileProgressWrapper.offsetHeight;

}
FileProgress.prototype.setProgress = function (percentage) {
	this.fileProgressElement.className = "progressContainer green";
	this.fileProgressElement.childNodes[3].className = "progressBarInProgress";
	this.fileProgressElement.childNodes[3].style.width = percentage + "%";
};
FileProgress.prototype.setComplete = function () {
	this.fileProgressElement.className = "progressContainer blue";
	this.fileProgressElement.childNodes[3].className = "progressBarComplete";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setError = function () {
	this.fileProgressElement.className = "progressContainer red";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setCancelled = function () {
	this.fileProgressElement.className = "progressContainer";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setStatus = function (status) {
	this.fileProgressElement.childNodes[2].innerHTML = status;
};

FileProgress.prototype.toggleCancel = function (show, swfuploadInstance) {
	this.fileProgressElement.childNodes[0].style.visibility = show ? "visible" : "hidden";
	if (swfuploadInstance) {
		var fileID = this.fileProgressID;
		this.fileProgressElement.childNodes[0].onclick = function () {
			swfuploadInstance.cancelUpload(fileID);
			return false;
		};
	}
};
