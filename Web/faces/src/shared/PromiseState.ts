import { WritableSignal } from "@angular/core";
import { BehaviorSubject } from "rxjs";


export class PromiseState<TResult>{

    private _isDelivered = new BehaviorSubject<boolean>(false);

    public get isDelivered(): boolean {
        return this._isDelivered.getValue();
    }

    constructor(private promise: Promise<TResult>){
        promise.finally(() => this._isDelivered.next(true));
    }

    async awaitSafe(): Promise<{isSuccess: boolean, data?: TResult, error?: string}> {

        try{

            const result = await this.promise;

            return {
                isSuccess: true,
                data: result
            }

        }catch(err: any) {
            console.log(err);
            return {
                isSuccess: false,
                error: err?.error?.message
            }
        }
    }

    loadingSignal(loadingSig: WritableSignal<boolean>){
        this._isDelivered.subscribe({
            next: (isDelivered) => {
                loadingSig.set(!isDelivered)
            }
        })
    }

    loadingCallback(callback:(loading: boolean) => void){
        this._isDelivered.subscribe({
            next: (isDelivered) => {
                callback(isDelivered)
            }
        })
    }
}