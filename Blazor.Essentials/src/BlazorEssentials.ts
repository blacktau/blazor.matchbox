import { ResizeObserverManager } from "./ResizeObserverAPI/ResizeObserverManager";

namespace BlazorEssentials {
  const StaticName: string = 'BlazorEssentials';
  
  const BlazorEssentials = {
    ResizeObserverManager: new ResizeObserverManager()
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