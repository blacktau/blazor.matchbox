import { ResizeObserverManager } from './ResizeObserverAPI/ResizeObserverManager'
import {IntersectionObserverManager} from './IntersectionObserverAPI/IntersectionObserverManager'
import { LocalStorageProxy } from './WebStorage/LocalStorageProxy'
import { SessionStorageProxy } from './WebStorage/SessionStorageProxy'


const BlazorMatchboxInit = {
  BlazorMatchbox: {
    ResizeObserverManager: new ResizeObserverManager(),
    IntersectionObserverManager: new IntersectionObserverManager(),
    LocalStorageProxy: new LocalStorageProxy(),
    SessionStorageProxy: new SessionStorageProxy()
  },
  initialize: () => {
    const StaticName = 'BlazorMatchbox'
    if (typeof window != 'undefined' && !window[StaticName]) {
      window[StaticName] = {
        ...BlazorMatchboxInit.BlazorMatchbox
      }
    } else {
      window[StaticName] = {
        ...window[StaticName],
        ...BlazorMatchboxInit.BlazorMatchbox
      }
    }
  }
  
}

BlazorMatchboxInit.initialize()
