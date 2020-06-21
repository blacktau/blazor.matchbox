export class SessionStorageProxy {
    getLength = () : number => {
        return window.sessionStorage.length
    }

    key = (index: number) : string | null => {
        return window.sessionStorage.key(index)
    }

    getItem = (key: string) : string | null => { 
        return window.sessionStorage.getItem(key)
    }

    setItem = (key: string, value: string) : void => {
        window.sessionStorage.setItem(key, value)
    }

    removeItem = (key: string) : void => {
        window.sessionStorage.removeItem(key)
    }

    clear = () : void => {
        window.sessionStorage.clear()
    }
}