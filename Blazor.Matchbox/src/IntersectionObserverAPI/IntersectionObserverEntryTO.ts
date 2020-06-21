import { DOMRectReadOnlyTO } from '../Common/DOMRectReadOnlyTO'

export class IntersectionObserverEntryTO {
    boundingClientRect?: DOMRectReadOnlyTO
    intersectionRatio?: number
    intersectionRect?: DOMRectReadOnlyTO
    isIntersecting?: boolean
    rootBounds?: DOMRectReadOnlyTO
    target?: Element
    time?: number
}