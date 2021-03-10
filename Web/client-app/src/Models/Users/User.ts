export type TUser = {
    id: number;
    login: string;
    begin_date: Date;
    end_date: Date;
    is_blocked: boolean;
    required_password_change?: boolean;
    wrong_login_count: number;
    last_login_date: Date;
}

