import { reactive } from 'vue';

export const useToast = () => {
   const state = reactive({
      show: false,
      message: '',
      type: 'success'
   });

   function open(msg, t = 'success', duration = 2000) {
      state.message = msg;
      state.type = t;
      state.show = true;

      setTimeout(() => {
         state.show = false;
      }, duration);
   }

   return { state, open };
};