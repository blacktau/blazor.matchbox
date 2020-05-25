import { BoxSizeTO } from '../Common/BorderBoxSizeTO'
import { DOMRectReadOnlyTO } from '../Common/DOMRectReadOnlyTO'

export class ResizeObserverEntryTO {
  borderBoxSize?: BoxSizeTO
  contentBoxSize?: BoxSizeTO
  contentRect?: DOMRectReadOnlyTO
  target?: Element
}