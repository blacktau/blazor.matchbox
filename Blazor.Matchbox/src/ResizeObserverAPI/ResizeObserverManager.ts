import { ResizeObserverEntryTO } from './ResizeObserverEntryTO'
import { BoxSizeTO } from '../Common/BorderBoxSizeTO'
import { DOMRectReadOnlyTO } from '../Common/DOMRectReadOnlyTO'

  export class ResizeObserverManager {
    private resizeObservers: Map<string, ResizeObserver> = new Map<string, ResizeObserver>()
    private dotnetObservers: Map<string, IDotnetResizeObserver> = new Map<string, IDotnetResizeObserver>()

    create = (instanceKey: string, dotnetInstanceRef: IDotnetResizeObserver): void => {
      const observer = new ResizeObserver(this.observerCallBack)
      this.resizeObservers.set(instanceKey, observer)
      this.dotnetObservers.set(instanceKey, dotnetInstanceRef)
    }

    disconnect = (instanceKey: string): void => {
      const observer = this.resizeObservers.get(instanceKey)
      if (observer) {
        observer.disconnect()
      }
    }

    observe = (instanceKey: string, element: Element): void => {
      const observer = this.resizeObservers.get(instanceKey)
      if (observer) {
        observer.observe(element)
      }
    }

    unobserve = (instanceKey: string, element: Element): void => {
      const observer = this.resizeObservers.get(instanceKey)

      if (observer) {
        observer.unobserve(element)
      }
    }

    dispose = (instanceKey: string) : void => {
      this.disconnect(instanceKey)
      this.dotnetObservers.delete(instanceKey)
      this.resizeObservers.delete(instanceKey)
    }

    private getKeyForObserver = (observer: ResizeObserver): string => {
      let foundKey = ''
      this.resizeObservers.forEach((value, key) => {
        if (observer == value) {
          foundKey = key
        }
      })

      return foundKey
    }

    private observerCallBack = (entries, observer) => {
      const key = this.getKeyForObserver(observer)

      if (!key || key.length == 0) {
        return
      }

      const dotNetInstance = this.dotnetObservers.get(key)
      if (dotNetInstance) {
        const mappedEntries = new Array<ResizeObserverEntryTO>()
        entries.forEach(entry => {
          if (entry) {
            const mEntry = new ResizeObserverEntryTO()
            if (entry.borderBoxSize) {
              mEntry.borderBoxSize = new BoxSizeTO()
              mEntry.borderBoxSize.blockSize = entry.borderBoxSize.blockSize
              mEntry.borderBoxSize.inlineSize = entry.borderBoxSize.inlineSize
            }

            if (entry.contentBoxSize) {
              mEntry.contentBoxSize = new BoxSizeTO()
              mEntry.contentBoxSize.blockSize = entry.contentBoxSize.blockSize
              mEntry.contentBoxSize.inlineSize = entry.contentBoxSize.inlineSize
            }

            if (entry.contentRect) {
              mEntry.contentRect = new DOMRectReadOnlyTO()
              mEntry.contentRect.x = entry.contentRect.x
              mEntry.contentRect.y = entry.contentRect.y
              mEntry.contentRect.width = entry.contentRect.width
              mEntry.contentRect.height = entry.contentRect.height
              mEntry.contentRect.top = entry.contentRect.top
              mEntry.contentRect.right = entry.contentRect.right
              mEntry.contentRect.bottom = entry.contentRect.bottom
              mEntry.contentRect.left = entry.contentRect.left
            }

            mEntry.target = entry.target
            mappedEntries.push(mEntry)
          }
        })

        const entriesJson = JSON.stringify(mappedEntries)       
        dotNetInstance.invokeMethodAsync('InvokeCallback', entriesJson)
      }
    }
  }

  interface IDotnetResizeObserver {
    invokeMethodAsync(methodName: string, entries: string) : void
  }