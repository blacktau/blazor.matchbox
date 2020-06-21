export class LocalStorageProxy {
    getLength = () : number => {
        return window.localStorage.length
    }

    key = (index: number) : string | null => {
        return window.localStorage.key(index)
    }

    getItem = (key: string) : string | null => { 
        return window.localStorage.getItem(key)
    }

    setItem = (key: string, value: string) : void => {
        window.localStorage.setItem(key, value)
    }

    removeItem = (key: string) : void => {
        window.localStorage.removeItem(key)
    }

    clear = () : void => {
        window.localStorage.clear()
    }
}