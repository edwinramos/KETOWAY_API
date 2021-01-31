import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Cookies from 'js-cookie';

export const toastType = {
    SUCCESS: "success",
    ERROR: "error",
    WARNING: "warning",
    INFO: "info",
}

export const PostData = async (url, body) => {
    try {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(body)
        };
        var result = await fetch(url, requestOptions)
            .then(res => res.json())
            .then((myJson) => {
                if (myJson.success)
                    return myJson.payload;
                else {
                    showToast(myJson.message, toastType.ERROR);
                    return null;
                }
            });
        return result;
    } catch (ex) {
        showToast(ex.message, toastType.ERROR);
    }
}
export const DeleteData = async (url) => {
    try {
        const requestOptions = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        };
        var result = await fetch(url, requestOptions)
            .then(res => res.json())
            .then((myJson) => {
                if (myJson.success)
                    return myJson.payload;
                else {
                    showToast(myJson.message, toastType.ERROR);
                    return null;
                }
            });
        return result;
    } catch (ex) {
        showToast(ex.message, toastType.ERROR);
    }
}

export const GetData = async (url) => {
    try {
        var result = await fetch(url)
            .then(res => res.json())
            .then((myJson) => {
                if (myJson.success)
                    return myJson.body;
                else {
                    showToast(myJson.message, toastType.ERROR);
                    return null;
                }
            });
        return result;
    } catch (ex) {
        showToast(ex.message, toastType.ERROR);
    }
}
export const showToast = (msg, type) => {
    switch (type) {
        case toastType.SUCCESS:
            toast.success(msg);
            break;
        case toastType.ERROR:
            toast.error(msg);
            break;
        case toastType.WARNING:
            toast.warning(msg);
            break;
        case toastType.INFO:
            toast.info(msg);
            break;
        default:
    }
}

export const getCookie = (key) => {
    return Cookies.get(key)
}

export const setCookie = (key, value) => {
    Cookies.set(key, value, { expires: 1 });
}

