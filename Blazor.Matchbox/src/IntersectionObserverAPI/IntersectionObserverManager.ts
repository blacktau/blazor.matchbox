import { IntersectionObserverEntryTO } from './IntersectionObserverEntryTO'
import { DOMRectReadOnlyTO } from '../Common/DOMRectReadOnlyTO'

  export class IntersectionObserverManager {
    private observers: Map<string, IntersectionObserver> = new Map<string, IntersectionObserver>()
    private dotnetObservers: Map<string, IDotnetIntersectionObserver> = new Map<string, IDotnetIntersectionObserver>()

    create = (instanceKey: string, dotnetInstanceRef: IDotnetIntersectionObserver, options?: IntersectionObserverOptions) : void => {
      const observer = new IntersectionObserver(this.observerCallBack, options)
      this.observers.set(instanceKey, observer)
      this.dotnetObservers.set(instanceKey, dotnetInstanceRef)
    }

    disconnect = (instanceKey: string) : void => {
      const observer = this.observers.get(instanceKey)
      if (observer) {
        observer.disconnect()
      }
    }

    observe = (instanceKey: string, element: Element) : void => {
      const observer = this.observers.get(instanceKey)

      if (observer) {
        observer.observe(element)
      }
    }

    unobserve = (instanceKey: string, element: Element) : void => {
      const observer = this.observers.get(instanceKey)

      if (observer) {
        observer.unobserve(element)
      }
    }

    takeRecords = (instanceKey: string) : string | undefined => {
      const observer = this.observers.get(instanceKey)

      if (observer) {
        const entries = observer.takeRecords()
        const mappedEntries = this.convertEntries(entries)

        return JSON.stringify(mappedEntries)
      }      
    }

    dispose = (instanceKey: string) : void => {
      this.disconnect(instanceKey)
      this.dotnetObservers.delete(instanceKey)
      this.observers.delete(instanceKey)
    }

    private getKeyForObserver = (observer: IntersectionObserver): string => {
      let foundKey = ''
      this.observers.forEach((value, key) => {
        if (observer == value) {
          foundKey = key
        }
      })

      return foundKey
    }

    private observerCallBack = (entries: IntersectionObserverEntry[], observer: IntersectionObserver) => {
      const key = this.getKeyForObserver(observer)

      if (!key || key.length == 0) {
        return
      }

      const dotNetInstance = this.dotnetObservers.get(key)
      if (dotNetInstance) {
        const mappedEntries = this.convertEntries(entries)
        const entriesJson = JSON.stringify(mappedEntries)
        dotNetInstance.invokeMethodAsync('InvokeCallback', entriesJson)
      }
    }

    private convertEntries = (entries: IntersectionObserverEntry[]): IntersectionObserverEntryTO[] => {
      const mappedEntries = new Array<IntersectionObserverEntryTO>()
      if (!entries) {
        return mappedEntries
      }

      entries.forEach((entry: IntersectionObserverEntry) => {
        if (entry) {
          const mEntry = new IntersectionObserverEntryTO()
          if (entry.boundingClientRect) {
            mEntry.boundingClientRect = this.convertDOMReadOnlyRect(entry.boundingClientRect)
          }

          if (entry.intersectionRect) {
            mEntry.intersectionRect = this.convertDOMReadOnlyRect(entry.intersectionRect)
          }

          if (entry.rootBounds) {
            mEntry.rootBounds = this.convertDOMReadOnlyRect(entry.rootBounds)
          }

          mEntry.intersectionRatio = entry.intersectionRatio
          mEntry.isIntersecting = entry.isIntersecting
          mEntry.time = entry.time
          mEntry.target = entry.target
          mappedEntries.push(mEntry)
        }
      })

      return mappedEntries
    }

    private convertDOMReadOnlyRect = (source: DOMRectReadOnly | ClientRect | DOMRect): DOMRectReadOnlyTO => {
      const target = new DOMRectReadOnlyTO()
      target.width = source.width
      target.height = source.height
      target.top = source.top
      target.right = source.right
      target.bottom = source.bottom
      target.left = source.left
      return target
    }
  }

  interface IDotnetIntersectionObserver {
    invokeMethodAsync(methodName: string, entries: string) : void
  }

  interface IntersectionObserverOptions {
    root?: Element
    rootMargin?: string
    threshold?: number | number[]
  }