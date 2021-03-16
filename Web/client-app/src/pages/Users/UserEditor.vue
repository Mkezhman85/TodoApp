<template>
  <q-dialog persistent v-model="userEditorOpened">
    <q-card style="max-width: 800px; width: 60vw">
      <q-bar class="bg-primary">
        <div class="text-h6 text-white">Просмотр пользователя</div>
        <q-space />
        <q-btn dense flat icon="close" @click="hideUserEditor">
          <q-tooltip>Close</q-tooltip>
        </q-btn>
      </q-bar>

      <q-card-section>
        <div class="row">
          <div class="col">
            <q-input
              label="Логин пользователя"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.login"
              :rules="[(val) => val !== null || 'Введите логин пользователя']"
              :readonly="readonlyParam"
            />
            <q-checkbox
              label="Отметка блокировки"
              v-model="user.is_blocked"
              :disable="readonlyParam"
            />
          </div>
          <div class="col">
            <q-input
              label="Дата начала доступа"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.begin_date"
              mask="date"
              :rules="['date']"
              :readonly="readonlyParam"
            >
              <template v-slot:append>
                <q-icon name="event" class="cursor-pointer">
                  <q-popup-proxy
                    ref="qDateProxy"
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="user.begin_date">
                      <div class="row items-center justify-end">
                        <q-btn v-close-popup label="Закрыть" color="primary" flat />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </template>
            </q-input>
            <q-input
              label="Дата окончания доступа"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.end_date"
              mask="date"
              :rules="['date']"
              :readonly="readonlyParam"
            >
              <template v-slot:append>
                <q-icon name="event" class="cursor-pointer">
                  <q-popup-proxy
                    ref="qDateProxy"
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="user.end_date">
                      <div class="row items-center justify-end">
                        <q-btn v-close-popup label="Закрыть" color="primary" flat />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </template>
            </q-input>
          </div>
        </div>
        <div class="row">
          <div class="col">
            <q-checkbox
              label="Требуется смена пароля"
              v-model="user.required_password_change"
              :disable="readonlyParam"
            />
          </div>
          <div class="col">
            <q-input
              label="wrong_login_count"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.wrong_login_count"
              type="number"
              :readonly="readonlyParam"
            />
            <q-input
              label="Дата последней авторизации"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.last_login_date"
              mask="date"
              :rules="['date']"
              :readonly="readonlyParam"
            >
              <template v-slot:append>
                <q-icon name="event" class="cursor-pointer">
                  <q-popup-proxy
                    ref="qDateProxy"
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="user.last_login_date">
                      <div class="row items-center justify-end">
                        <q-btn v-close-popup label="Закрыть" color="primary" flat />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </template>
            </q-input>
          </div>
        </div>
      </q-card-section>

      <q-separator />

      <q-card-actions class="row no-wrap">
        <q-space />

        <q-btn color="positive" label="сохранить" @click="CreateNewUser(user)" />

        <q-btn
          class="q-ml-sm"
          flat
          color="red"
          label="сбросить изменения"
          @click="resetUserInfo(user)"
        />
      </q-card-actions>

      <q-inner-loading>
        <q-spinner-gears size="48px" color="primary" />
      </q-inner-loading>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Component, Prop, Vue, PropSync } from "vue-property-decorator";
import { TSelect, TInput, TCheckbox } from "src/types";
import axios from "axios";
import { type } from "os";

@Component
export default class UserEditor extends Vue {
  @PropSync("value", { type: Boolean }) userEditorOpened!: string;
  // @Prop(String) mode!: string;
  @Prop(Boolean) readonlyParam!: string;

  // availabilityFields(mode: string) {
  //   if
  // }

  openUserEditor() {
    this.$emit("open-userEditorOpened", this.userEditorOpened);
  }
  hideUserEditor() {
    this.$emit("hide-userEditorOpened", this.userEditorOpened);
  }

  @Prop(Object) user: {
    // login: string;
    // is_blocked: boolean;
    // required_password_change: boolean;
    // wrong_login_count: number;
    // begin_date: Date;
    // end_date: Date;
    // last_login_date: Date;
  };

  CreateNewUser(user: object) {
    this.$emit("create-newUser", this.user);

    // this.user.login = "";
    // this.user.is_blocked = false;
    // this.user.required_password_change = false;
    // this.user.wrong_login_count = null;
    // this.user.begin_date = null;
    // this.user.end_date = null;
    // this.user.last_login_date = null;

    console.log(user);
  }

  resetUserInfo(user: object) {
    this.user.login = "";
    this.user.begin_date = "";
    this.user.end_date = "";
  }

  requiredPasswordChange = [true, false];

  // showNewUserConsole(user: object) {
  //   user = this.user;
  //   console.log(user);
  // }
}
</script>
