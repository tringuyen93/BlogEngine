import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { LocalStoreManager } from "./local-store-manager";
import { DBkeys } from "./dbkey";

@Injectable()
export class ConfigurationService{
    public baseUrl = environment.baseUrl;// || Utilities.baseUrl();
    public loginUrl = environment.loginUrl;
    // constructor(private localStorage: LocalStoreManager) {
    //     this.loadLocalChanges();
    // }

    // private loadLocalChanges() {
    //     if (this.localStorage.exists(DBkeys.HOME_URL))
    //         this._homeUrl = this.localStorage.getDataObject<string>(DBkeys.HOME_URL);

    //     if (this.localStorage.exists(DBkeys.THEME))
    //         this._theme = this.localStorage.getDataObject<string>(DBkeys.THEME);

    //     if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_STATISTICS))
    //         this._showDashboardStatistics = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_STATISTICS);

    //     if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_NOTIFICATIONS))
    //         this._showDashboardNotifications = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_NOTIFICATIONS);

    //     if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_TODO))
    //         this._showDashboardTodo = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_TODO);

    //     if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_BANNER))
    //         this._showDashboardBanner = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_BANNER);
    // }
    // public clearLocalChanges() {
    //     this._language = null;
    //     this._homeUrl = null;
    //     this._theme = null;
    //     this._showDashboardStatistics = null;
    //     this._showDashboardNotifications = null;
    //     this._showDashboardTodo = null;
    //     this._showDashboardBanner = null;

    //     this.localStorage.deleteData(DBkeys.LANGUAGE);
    //     this.localStorage.deleteData(DBkeys.HOME_URL);
    //     this.localStorage.deleteData(DBkeys.THEME);
    //     this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_STATISTICS);
    //     this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_NOTIFICATIONS);
    //     this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_TODO);
    //     this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_BANNER);

    //     this.resetLanguage();
    // }
}