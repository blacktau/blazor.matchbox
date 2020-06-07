import { ResizeObserverManager } from './ResizeObserverAPI/ResizeObserverManager'
import {IntersectionObserverManager} from './IntersectionObserverAPI/IntersectionObserverManager'
import { LocalStorageProxy } from './WebStorage/LocalStorageProxy'
import { SessionStorageProxy } from './WebStorage/SessionStorageProxy'


const BlazorEssentialsInit = {
  BlazorEssentials: {
    ResizeObserverManager: new ResizeObserverManager(),
    IntersectionObserverManager: new IntersectionObserverManager(),
    LocalStorageProxy: new LocalStorageProxy(),
    SessionStorageProxy: new SessionStorageProxy()
  },
  initialize: () => {
    const StaticName = 'BlazorEssentials'
    if (typeof window != 'undefined' && !window[StaticName]) {
      window[StaticName] = {
        ...BlazorEssentialsInit.BlazorEssentials
      }
    } else {
      window[StaticName] = {
        ...window[StaticName],
        ...BlazorEssentialsInit.BlazorEssentials
      }
    }
  }
  
}

BlazorEssentialsInit.initialize()
