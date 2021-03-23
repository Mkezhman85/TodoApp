export type TUser = {
    id: number;
    login: string;
    beginDate: Date;
    endDate: Date;
    isBlocked: boolean;
    requiredPasswordChange?: boolean;
    wrongLoginCount: number;
    last_login_date: Date;
}

