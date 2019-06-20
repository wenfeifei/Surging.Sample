import {
  getUserInfo,
} from '@/api/user'

import {
  setToken,
  getToken
} from '@/libs/util'
import {
  resolve
} from 'url';

export default {
  state: {
    userName: '',
    userId: '',
    token: getToken(),
    access: '',
    hasGetInfo: false
  },
  mutations: {
    setUserId(state, id) {
      state.userId = id
    },
    setUserName(state, name) {
      state.userName = name
    },
    setAccess(state, access) {
      state.access = access
    },
    setToken(state, token) {
      state.token = token
      setToken(token)
    },
    setHasGetInfo(state, status) {
      state.hasGetInfo = status
    }
  },
  actions: {
    getUserInfo({
      state,
      commit
    }) {
      return new Permise((resolve, reject) => {
        try {
          getUserInfo(state.token).then(res => {
            const data = res.data
            commit('setUserName', data.name)
            commit('setUserId', data.user_id)
            commit('setAccess', data.access)
            commit('setHasGetInfo', true)
            resolve(data)
          }).catch(err => {
            reject(err)
          })
        } catch (error) {
          reject(error)
        }
      })
    }
  }
}
