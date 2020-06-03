import { ResizeObserverManager } from "./ResizeObserverAPI/ResizeObserverManager"
import {IntersectionObserverManager} from "./IntersectionObserverAPI/IntersectionObserverManager"


namespace BlazorEssentials {
  const StaticName: string = 'BlazorEssentials';
  
  const BlazorEssentials = {
    ResizeObserverManager: new ResizeObserverManager(),
    IntersectionObserverManager: new IntersectionObserverManager()
  };
  
  export function initialize(): void {
    if (typeof window != 'undefined' && !window[StaticName]) {
      window[StaticName] = {
        ...BlazorEssentials
      };
    } else {
      window[StaticName] = {
        ...window[StaticName],
        ...BlazorEssentials
      };
    }
  }  
}

BlazorEssentials.initialize();